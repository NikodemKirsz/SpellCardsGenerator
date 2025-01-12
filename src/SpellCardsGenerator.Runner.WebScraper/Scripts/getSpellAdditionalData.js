(() => {
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

  return {
    source: elements[1].textContent.substring(8),
    description: desc,
    higherLevelDesc: isHigherLevel ? lastDesc.substring(higherLevelText.length + 1) : null,
    hasVerbal: hasVerbal,
    hasSemantic: hasSemantic,
    hasMaterial: hasMaterial,
    materialComponents: materialComponents,
    containsHtml: containsHtml,
  };
})();