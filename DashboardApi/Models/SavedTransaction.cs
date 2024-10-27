using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DashboardApi.Models;

/// <summary>
/// Represents a Transaction
/// </summary>
public class SavedTransaction
{
    /// <summary>
    /// Unique ID
    /// </summary>
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Indicator of the transaction. Can be Debit or Credit
    /// </summary>
    [Column("indicator")]
    public string Indicator { get; set; } = null!;

    /// <summary>
    /// Type of transactionk
    /// </summary>
    [Column("type")]
    public string Type { get; set; } = null!;

    /// <summary>
    /// Transaction Dollar Amount
    /// </summary>
    [Column("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Transaction Description
    /// </summary>
    [Column("description")]
    public string Description { get; set; } = null!;

    /// <summary>
    /// Transaction Date
    /// </summary>
    [Column("booking_date")]
    public DateTime BookingDate { get; set; }

    /// <summary>
    /// Transaction ID
    /// </summary>
    [Column("category")]
    public string Category { get; set; } = null!;
}
