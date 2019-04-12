export function readFile(file, callback) {
  const fileReader = new FileReader();
  fileReader.onload = e => {
    const obj = JSON.parse(<string>fileReader.result);
    callback(obj);
  };
  fileReader.readAsText(file);
}
