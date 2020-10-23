using System;

namespace Domain
{
    /// <summary>
    /// Message functionality.
    /// </summary>
    public class LogMessage
    {
        private int _id;
        private DateTime _date;
        private string _content;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogMessage"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="date">The date.</param>
        /// <param name="content">The content.</param>
        /// <exception cref="System.ArgumentNullException">content</exception>
        public LogMessage(
            int id,
            DateTime date,
            string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            _id = id;
            _date = date;
            _content = content;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A formatted <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"Message contains Id: {_id}, Date: {_date}, Content: {_content}";
        }
    }
}
