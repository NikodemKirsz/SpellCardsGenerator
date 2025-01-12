namespace SpellCardsGenerator.Common.Models;

public readonly struct SpellComponents : IEquatable<SpellComponents>
{
  public const string DefaultSeparator = ", ";
  public const bool DefaultShowMaterialComponents = true;
  private const int VerbalIndex = 0; 
  private const int SemanticIndex = 1; 
  private const int MaterialIndex = 2; 

  public readonly string? MaterialComponents;
  private readonly string?[] Components;

  public string? Verbal => Components[VerbalIndex];
  public string? Semantic => Components[SemanticIndex];
  public string? Material => Components[MaterialIndex];

  public SpellComponents(
    string? verbal,
    string? semantic,
    string? material,
    string? materialComponents)
  {
    if (material is not null && materialComponents is null)
      throw new ArgumentException(
        $"{nameof(MaterialComponents)} is null when {nameof(Material)} is present!",
        nameof(materialComponents)
      );

    Components = [verbal, semantic, material];
    MaterialComponents = materialComponents;
  }

  public static bool operator ==(SpellComponents left, SpellComponents right)
  {
    return left.Equals(right);
  }

  public static bool operator !=(SpellComponents left, SpellComponents right)
  {
    return !(left == right);
  }

  public override bool Equals(object? obj)
  {
    return obj is SpellComponents other && Equals(other);
  }

  public bool Equals(SpellComponents other)
  {
    return Verbal == other.Verbal &&
     Semantic == other.Semantic &&
     Material == other.Material &&
     MaterialComponents == other.MaterialComponents;
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(Verbal, Semantic, Material, MaterialComponents);
  }

  public override string ToString()
  {
    return ToString(DefaultSeparator, DefaultShowMaterialComponents);
  }

  public string ToString(string separator, bool showMaterialComponents)
  {
    string components = String.Join(separator, Components.Where(s => s is not null) );
    if (Material is not null && showMaterialComponents)
      components = $"{components} ({MaterialComponents})";

    return components;
  }
}