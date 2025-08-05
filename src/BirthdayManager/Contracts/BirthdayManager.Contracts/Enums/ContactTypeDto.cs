namespace BirthdayManager.Contracts.Enums;

/// <summary>
/// Тип контакта.
/// <remarks>
/// 0 - Знакомые.
/// 1 - Родственники.
/// 2 - Друзья.
/// 3 - Коллеги. 
/// </remarks>
/// </summary>
public enum ContactTypeDto
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