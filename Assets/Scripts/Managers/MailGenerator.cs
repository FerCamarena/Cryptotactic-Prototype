
using System.Collections.Generic;
using UnityEngine;
using System;

namespace App.Game.Mail {
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class MailGenerator {
    // ? DEBUG======================================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private bool DEBUG = false;

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        // [Header("References")]

        // * ATTRIBUTES
        [Header("Attributes")]
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private List<string> greetings;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private List<string> summaries;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private List<string> authors;
        
        // * INTERNALS
        [Header("Internals")]
        /// <summary>
        /// 
        /// </summary>
        [NonSerialized] private int idCounter;

    // ? BASE METHODS===============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        public MailGenerator() {
            this.idCounter = 0;
        }

    // ? CUSTOM METHODS=============================================================================================================================
    
    // ? EVENT METHODS==============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        public MailData OnGenerateMailData() {
            if (this.greetings == null || this.summaries == null || this.authors == null)
                return default;

            if (this.greetings.Count == 0 || this.summaries.Count == 0 || this.authors.Count == 0)
                return default;

            string greeting = this.greetings[UnityEngine.Random.Range(0, this.greetings.Count)];
            string subject  = this.summaries[UnityEngine.Random.Range(0, this.summaries.Count)];
            string author   = this.authors[UnityEngine.Random.Range(0, this.authors.Count)];

            MailData data = new MailData {
                id = (++this.idCounter).ToString(),
                subject = subject,
                body = $"{greeting},\n\n{subject}.",
                author = author,
            };

            if (this.DEBUG)
                Debug.Log($"[MG] Generated Mail: {data.id}");

            return data;
        }

    }
}