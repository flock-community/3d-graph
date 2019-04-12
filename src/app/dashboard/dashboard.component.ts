import { Component, OnInit } from '@angular/core';
import { GraphParser } from '../helper/graphParser';
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
  constructor() {
    this.groups = {
      app: { color: 'green', size: 20 },
      app_true: { color: 'green', size: 20 },
      app_false: { color: 'blue', size: 20 },
      orange: { color: 'purple', size: 10 },
      ui: { color: 'gray', size: 6 },
      util: { color: 'gray', size: 6 },
      guide: { color: 'gray', size: 30 },
      web: { color: 'orange', size: 30 },
    };
  }

  graphParser: GraphParser;
  file: string;
  graph: Graph;
  forceGraph: any;
  groups: any;

  style: 'nodes' | 'labels';
  nodeSize: number;
  dependenciesOf: string;
  dependantsOf: string;

  ngOnInit() {
    this.graphParser = new GraphParser();
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
    this.graphParser.readFile(this.file, this.newGraph.bind(this));
  }

  newGraph(g: Graph) {
    this.graph = g;
    this.updateGraph();
  }

  nodeSizeChanged(e) {
    this.nodeSize = e.target.value;
    if (this.forceGraph) {
      this.forceGraph.nodeRelSize(this.nodeSize);
    }
  }

  highlightChanged(e) {
    // const prefix = e.target.value;
    this.forceGraph.nodeColor(node => {
      const group = this.calculateGroup(node.id);
      return this.groups[group.name] ? this.groups[group.name].color : 'red';
    });
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
    this.forceGraph = ForceGraph3D()(document.getElementById('threed-graph'))
      .graphData(this.graph.data)
      .nodeVal(this.nodeSize)
      .backgroundColor('white')
      .linkColor('black')
      .linkOpacity(0.5)
      .nodeLabel(node => `<font color="black">${node.name}</font>`);
  }

  showAsLabels() {
    this.forceGraph = ForceGraph3D()(document.getElementById('threed-graph'))
      .graphData(this.graph.data)
      .nodeVal(this.nodeSize)
      .backgroundColor('white')
      .nodeAutoColorBy('group')
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

  // TODO: load this from a file instead
  calculateGroup(name) {
    const groups = {
      'the-guide-': {
        name: 'guide',
        level: 3,
        mass: 0.2,
      },
      'ing-app-': {
        name: 'app',
        level: 1,
        mass: 0.5,
      },
      'ing-orange-': {
        name: 'orange',
        level: 2,
        mass: 0.75,
      },
      'ing-uif-': {
        name: 'ui',
        level: 3,
        mass: 1,
      },
      'ing-uic-': {
        name: 'ui',
        level: 3,
        mass: 1,
      },
      'ing-util-': {
        name: 'util',
        level: 3,
        mass: 1,
      },
      'ing-kit-': {
        name: 'util',
        level: 3,
        mass: 1,
      },
      'ing-web': {
        name: 'web',
        level: 3,
        mass: 1,
      },
    };

    const res = Object.keys(groups).find(key => new RegExp(key).test(name));
    return groups[res] || {};
  }
}
