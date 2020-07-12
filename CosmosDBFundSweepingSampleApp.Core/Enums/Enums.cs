using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace CosmosDBFundSweepingSampleApp.Core.Enums
{
    public enum SettlementMode
    {
        [Description("Bulk")]
        Bulk = 1,

        [Description("LineByLine")]
        LineByLine = 2,
    }

    public enum SettlementShareType
    {
        [Description("Percentage")]
        Percentage = 1,

        [Description("Flat")]
        Flat = 2,

        [Description("Hybrid")]
        Hybrid = 3,
    }

    public enum RequeryStatus
    {
        [Description("Pending")]
        Pending = 1,

        [Description("Queried")]
        Queried = 2,
    }

    public enum PaymentStatus
    {
        [Description("WAITING")]
        Waiting = 1,

        [Description("DECLINED")]
        Declined = 2,

        [Description("FAILED")]
        Failed = 3,

        [Description("PAID")]
        Paid = 4,
    }


    public enum ErrorCode
    {
        /// <summary>
        /// General exception
        /// </summary>
        EC000,

        /// <summary>
        /// Model is empty
        /// </summary>
        EC001,

        /// <summary>
        /// Record already exist
        /// </summary>
        EC002,

        /// <summary>
        /// Invalid Hmac value
        /// </summary>
        EC003,

        /// <summary>
        /// Reference Number already exist
        /// </summary>
        EC004,

        /// <summary>
        /// Record not found
        /// </summary>
        EC005,

        /// <summary>
        /// Computed and split amount does not not match
        /// </summary>
        EC006,

        /// <summary>
        /// Computed amount for a participant resulted into negative value
        /// </summary>
        EC007,


    }

    public static class EnumHelper
    {
        public static string ToDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

    }

}
