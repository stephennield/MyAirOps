using System;
using System.ComponentModel.DataAnnotations;

namespace MyAirOpsLogger.Models
{
    /// <summary>
    /// API model for posting the message.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// ets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        [Required]
        [MaxLength(255, ErrorMessage = "Must be no more than 255 characters")]
        public string Content { get; set; }
    }
}
