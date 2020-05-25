using System;

namespace CV19.Models
{
    /// <summary>
    /// Количество подтвержденных заболевших
    /// </summary>
    internal struct ConfirmedCount
    {
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Количество заболевших
        /// </summary>
        public int Count { get; set; }
    }
}