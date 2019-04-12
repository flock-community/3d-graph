import { Graph } from './graph';

export class GraphParser {
  readFile(file, callback) {
    const fileReader = new FileReader();
    fileReader.onload = e => {
      const json = JSON.parse(<string>fileReader.result);
      const data = this.parseData(json);
      callback(new Graph(data));
    };
    fileReader.readAsText(file);
  }

  parseData(data) {
    const ret = { nodes: [], links: [] };
    ret.nodes = data.nodes.map(node => {
      return { id: node.id, name: node.label, val: 5 };
    });
    ret.links = data.edges.map(edge => {
      return { source: edge.from, target: edge.to };
    });
    return ret;
  }
}
