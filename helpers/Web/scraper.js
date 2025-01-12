function sleep(ms) {
  return new Promise(resolve => setTimeout(resolve, ms));
}

function getSpells() {
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
}

function getSpellAdditionalData() {
  const higherLevelText = 'At Higher Levels.';
  const materialPhrase = 'M (';
  
  const spell = document.getElementById('page-content');
  const elements = Array.from(spell.children);
  
  const components = elements[3].childNodes[9].textContent;
  const materialsIndex = components.indexOf(materialPhrase);
  const hasMaterial = materialsIndex >= 0;
  let materialComponents = null, shortComponents = components;
  if (hasMaterial) {
    shortComponents = components.substring(0, materialsIndex);
    materialComponents = components.substring(materialsIndex + 3, components.length - 1);
  }
  const hasVerbal = shortComponents.indexOf('V') >= 0;
  const hasSemantic = shortComponents.indexOf('S') >= 0;
  
  const lastDescElementIndex = elements.length - 3;
  
  const lastDesc = elements[lastDescElementIndex].textContent;
  const isHigherLevel = lastDesc.startsWith(higherLevelText);
  const descriptionElements = elements.slice(4, elements.length - (isHigherLevel ? 3 : 2));
  
  const desc = descriptionElements
    .map(descElement => descElement.textContent)
    .join('<br>\n');
  const containsHtml = desc.indexOf('\n\n') >= 0;
  
  const spellData = {
    source: elements[1].textContent.substring(8),
    description: desc,
    higherLevelDesc: isHigherLevel ? lastDesc.substring(higherLevelText.length + 1) : null,
    hasVerbal: hasVerbal,
    hasSemantic: hasSemantic,
    hasMaterial: hasMaterial,
    materialComponents: materialComponents,
    containsHtml: containsHtml,
  };
  
  return spellData;
}