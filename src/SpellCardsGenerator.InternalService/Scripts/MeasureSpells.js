Array
  .from(document.getElementsByClassName('spell'))
  .map(spell => {
    return {
      slug: spell.id,
      height: spell.offsetHeight,
    }
  });