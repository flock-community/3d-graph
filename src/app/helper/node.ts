export class Node {
  id: string;
  name: string;
  dependencies: Node[];
  dependants: Node[];

  constructor(id: string, name: string) {
    this.id = id;
    this.name = name;
  }
}
