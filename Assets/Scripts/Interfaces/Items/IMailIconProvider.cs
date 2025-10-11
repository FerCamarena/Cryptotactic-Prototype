using App.Game.Mail;
using UnityEngine;

namespace App.Game.Managers {
    /// <summary>
    /// 
    /// </summary>
    public interface IMailIconProvider {
    // ? CUSTOM METHODS=============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        GameObject OnSpawnMailIcon(MailData data);

        /// <summary>
        /// 
        /// </summary>
        GameObject GetMailIcon();
    }
}