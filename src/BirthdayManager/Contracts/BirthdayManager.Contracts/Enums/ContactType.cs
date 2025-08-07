namespace BirthdayManager.Contracts.Enums;

/// <summary>
/// Тип контакта.
/// 0 - Знакомые. 1 - Родственники. 2 - Друзья. 3 - Коллеги.
/// </summary>
public enum ContactType
{
    /// <summary>
    /// Знакомые,
    /// </summary>
    Acquaintances,

    /// <summary>
    /// Родственники.
    /// </summary>
    Relatives,

    /// <summary>
    /// Друзья.
    /// </summary>
    Friends,

    /// <summary>
    /// Коллеги.
    /// </summary>
    Colleagues
}