(() => {
  const spells = [];

  const tablesContainers = document.getElementsByClassName('list-pages-box');
  for (let i = 0; i < tablesContainers.length; i++) {
    const table = tablesContainers[i].children[0];
    const body = table.children[0];
    const tableRows = Array.from(body.children).slice(1);

    for (let j = 0; j < tableRows.length; j++) {
      const tableRow = tableRows[j];

      const aNode = tableRow.children[0].children[0];
      const spell = {
        name: aNode.textContent,
        level: i,
        link: aNode.getAttribute('href'),
        school: tableRow.children[1].textContent,
        castingTime: tableRow.children[2].textContent,
        range: tableRow.children[3].textContent,
        duration: tableRow.children[4].textContent,
        components: tableRow.children[5].textContent,
      };
      spells.push(spell);
    }
  }

  return spells;
})();