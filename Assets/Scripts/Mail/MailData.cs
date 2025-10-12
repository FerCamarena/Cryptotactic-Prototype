using System;

namespace App.Game.Mail {
    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public struct MailData {
    // ? PARAMETERS===============================================================================================================================
        /// <summary>
        ///
        /// </summary>
        public string id;
        /// <summary>
        ///
        /// </summary>
        public string author;
        /// <summary>
        ///
        /// </summary>
        public string subject;
        /// <summary>
        ///
        /// </summary>
        public string body;

        public MailData(string id, string author, string subject, string body) {
            this.id = id ?? "X";
            this.author = author ?? "X";
            this.subject = subject ?? "X";
            this.body = body ?? "X";
        }
    }
}