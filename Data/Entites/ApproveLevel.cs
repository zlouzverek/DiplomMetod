using System.ComponentModel;

namespace DiplomMetod.Data.Entites
{
    public enum ApproveLevel

    {
        /// <summary>
        /// На согласовании
        /// </summary>
        [Description("Федеральный")]
        Federal = 0,

        /// <summary>
        /// Одобрено
        /// </summary>
        [Description("Местный")]
        Local = 1,

    }

}
