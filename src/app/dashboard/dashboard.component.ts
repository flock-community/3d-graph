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
  constructor() {}

  file: string;
  graph: Graph;
  forceGraph: any;
  colorScheme: any;

  style: 'nodes' | 'labels';
  nodeSize: number;
  dependenciesOf: string;
  dependantsOf: string;

  ngOnInit() {
    this.style = 'nodes';
    this.nodeSize = 3;
    this.dependenciesOf = '';
    this.dependantsOf = '';
  }

  nodes() {
    this.style = 'nodes';
    this.updateGraph();
  }

  labels() {
    this.style = 'labels';
    this.updateGraph();
  }

  fileChanged(e) {
    this.file = e.target.files[0];
    this.reload(e);
  }

  reload(e) {
    this.dependantsOf = '';
    this.dependenciesOf = '';
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

  showDependencies(e) {
    this.dependantsOf = '';
    this.dependenciesOf = e.target.value;
    this.graph = this.graph.subGraphFrom(this.graph.findById(this.dependenciesOf), 'dependencies');
    this.updateGraph();
  }

  showDependants(e) {
    this.dependenciesOf = '';
    this.dependantsOf = e.target.value;
    this.graph = this.graph.subGraphFrom(this.graph.findById(this.dependantsOf), 'dependants');
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
      .linkOpacity(0.6);

    this.applyColorScheme();
  }
}
