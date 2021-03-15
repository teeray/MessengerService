using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Base
{
    /// <summary>
    /// Housekeeping, Identification, Concurrency
    /// </summary>
    public abstract class HIDC
    {
        /// <summary>
        /// Required id field
        /// </summary>
        [Key]
        public int Id { get; set; }
        #region States
        /// <summary>
        /// For checking if an object is deleted
        /// </summary>
        [Required]
        public bool IsDeleted { get; set; } = false;
        /// <summary>
        /// For checking if an object is disabled
        /// </summary>
        [Required]
        public bool IsDisabled { get; set; } = false;
        #endregion
        #region Insertion/Update
        /// <summary>
        /// Date the object was inserted into the db
        /// </summary>
        [Required]
        public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
        /// <summary>
        /// Date the object was last updated in the db
        /// </summary>
        [Required]
        public DateTimeOffset LastUpdated { get; set; } = DateTimeOffset.UtcNow;
        #endregion
        /// <summary>
        /// For concurrency, row version is updated with every db transaction. 
        /// If passed in objects row version doesn't match then the db doesn't change.
        /// </summary>
        [Timestamp]
        [Required]
        public byte[] RowVersion { get; set; }
    }
}
