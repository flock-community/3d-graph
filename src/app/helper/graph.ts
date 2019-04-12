import { Node } from './node';

export class Graph {
  nodes: Node[];
  data: any;

  constructor(data?: any, nodes?: Node[]) {
    this.data = data;
    this.nodes = nodes;

    if (data && !nodes) {
      this.populateFromData();
    } else if (nodes && !data) {
      this.generateData();
    }
  }

  populateFromData() {
    this.nodes = this.data.nodes.map(node => new Node(node.id, node.name));
    this.nodes.forEach(node => {
      node.dependencies = this.data.links
        .filter(link => link.source === node.id)
        .map(link => this.findById(link.target));

      node.dependants = this.data.links
        .filter(link => link.target === node.id)
        .map(link => this.findById(link.source));
    });
  }

  generateData() {
    this.data = { nodes: [], links: [] };
    this.data.nodes = this.nodes.map(node => {
      return { id: node.id, name: node.name, val: 5 };
    });
    this.data.links = this.nodes
      .map(node => {
        return node.dependencies
          .filter(nodeTarget => this.nodes.includes(nodeTarget))
          .map(nodeTarget => {
            return { source: node.id, target: nodeTarget.id };
          });
      })
      .reduce((acc, arr) => acc.concat(arr));
  }

  findById(id: string): Node {
    return this.nodes.find(node => node.id === id);
  }

  subGraphFrom(node: Node, relation: 'dependencies' | 'dependants') {
    const subNodes: Node[] = [];
    subNodes.push(node);
    console.log(relation);
    console.log(node[relation].length);
    const nodesTodo = Array.from(node[relation]);

    while (nodesTodo.length > 0) {
      const subject = nodesTodo.pop();
      subNodes.push(subject);
      subject[relation].forEach(target => {
        if (!nodesTodo.includes(target) && !subNodes.includes(target)) {
          nodesTodo.push(target);
        }
      });
    }

    return new Graph(null, subNodes);
  }
}
