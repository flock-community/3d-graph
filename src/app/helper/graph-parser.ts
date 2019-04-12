import { Graph } from './graph';

export function parseData(data) {
  const ret = { nodes: [], links: [] };
  ret.nodes = data.nodes.map(node => {
    return { id: node.id, name: node.label, val: 5 };
  });
  ret.links = data.edges.map(edge => {
    return { source: edge.from, target: edge.to };
  });
  return new Graph(ret);
}
