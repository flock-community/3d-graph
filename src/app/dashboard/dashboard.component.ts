import { ViewChild, ElementRef, Renderer2, HostListener } from '@angular/core';
import { readFile } from './../helper/json-file-reader';
import { Component, OnInit } from '@angular/core';
import { parseData } from '../helper/graph-parser';
import { Graph } from '../helper/graph';

import ForceGraph3D from '3d-force-graph';
import * as THREE from 'three';
import SpriteText from 'three-spritetext';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  constructor(private renderer: Renderer2) {}

  @ViewChild('menu') menu: ElementRef;

  file: string;
  mainGraph: Graph;
  graph: Graph;
  forceGraph: any;
  colorScheme: any;

  style: 'nodes' | 'labels';
  filter: 'dependencies' | 'dependants';
  nodeSize: number;
  filterValue: string;

  mousePosX: number;
  mousePosY: number;
  lastNodeClicked;

  ngOnInit() {
    this.style = 'nodes';
    this.nodeSize = 3;
    this.filterValue = '';
  }

  nodes() {
    this.style = 'nodes';
    this.updateGraph();
  }

  labels() {
    this.style = 'labels';
    this.updateGraph();
  }

  dependencies() {
    this.filter = 'dependencies';
  }

  dependants() {
    this.filter = 'dependants';
  }

  fileChanged(e) {
    this.file = e.target.files[0];
    this.reload(e);
  }

  reload(e) {
    this.filterValue = '';
    readFile(this.file, this.graphFileRead.bind(this));
  }

  colorSchemeChanged(e) {
    readFile(e.target.files[0], this.colorSchemeFileRead.bind(this));
  }

  nodeSizeChanged(e) {
    this.nodeSize = e.target.value;
    if (this.forceGraph) {
      this.forceGraph.nodeRelSize(this.nodeSize);
    }
  }

  doFilter(e) {
    this.filterValue = e.target.value;
    if (this.filterValue === '') {
      this.graph = this.mainGraph;
    } else {
      this.graph = this.mainGraph.subGraphFrom(
        this.mainGraph.findById(this.filterValue),
        this.filter,
      );
    }
    this.updateGraph();
  }

  @HostListener('document:mousemove', ['$event'])
  onMouseMove(e) {
    this.mousePosX = e.clientX;
    this.mousePosY = e.clientY;
  }

  showDependencies(e) {
    this.renderer.setStyle(this.menu.nativeElement, 'display', 'none');

    this.filterValue = this.lastNodeClicked.id;
    this.graph = this.mainGraph.subGraphFrom(
      this.mainGraph.findById(this.lastNodeClicked.id),
      'dependencies',
    );
    this.updateGraph();
  }

  showDependants(e) {
    this.renderer.setStyle(this.menu.nativeElement, 'display', 'none');

    this.filterValue = this.lastNodeClicked.id;
    this.graph = this.mainGraph.subGraphFrom(
      this.mainGraph.findById(this.lastNodeClicked.id),
      'dependants',
    );
    this.updateGraph();
  }

  colorSchemeFileRead(colorScheme) {
    this.colorScheme = colorScheme;
    this.applyColorScheme();
  }

  applyColorScheme() {
    if (this.colorScheme) {
      this.forceGraph.nodeColor(node => {
        const key = Object.keys(this.colorScheme.matchers).find(matcher =>
          new RegExp(matcher).test(node.id),
        );
        return (this.colorScheme.matchers[key] || this.colorScheme.default || {}).color;
      });
    }
  }

  graphFileRead(g: string) {
    this.newGraph(parseData(g));
  }

  newGraph(g: Graph) {
    this.mainGraph = g;
    this.graph = g;
    this.updateGraph();
  }

  updateGraph() {
    switch (this.style) {
      case 'nodes':
        this.showAsNodes();
        break;
      case 'labels':
        this.showAsLabels();
        break;
    }
  }

  showAsNodes() {
    this.applyDefaultSettings();
    this.forceGraph.nodeLabel(node => `<font color="black">${node.name}</font>`);
  }

  showAsLabels() {
    this.applyDefaultSettings();
    this.forceGraph
      .nodeLabel(node => '')
      .nodeThreeObject(node => {
        // use a sphere as a drag handle
        const obj = new THREE.Mesh(
          new THREE.SphereGeometry(10),
          new THREE.MeshBasicMaterial({
            depthWrite: false,
            transparent: true,
            opacity: 0,
          }),
        );
        // add text sprite as child
        const sprite = new SpriteText(node.id);
        sprite.color = node.color;
        sprite.textHeight = 8;
        obj.add(sprite);
        return obj;
      });

    // Spread nodes a little wider
    this.forceGraph.d3Force('charge').strength(-150);
  }

  applyDefaultSettings() {
    this.forceGraph = ForceGraph3D()(document.getElementById('threed-graph'))
      .graphData(this.graph.data)
      .nodeVal(this.nodeSize)
      .backgroundColor('white')
      .linkOpacity(0.6)
      .onNodeClick(node => {
        this.renderer.setStyle(this.menu.nativeElement, 'display', 'inline');
        this.renderer.setStyle(this.menu.nativeElement, 'top', `${this.mousePosY}px`);
        this.renderer.setStyle(this.menu.nativeElement, 'left', `${this.mousePosX + 20}px`);
        this.lastNodeClicked = node;
      });

    this.applyColorScheme();
  }
}
