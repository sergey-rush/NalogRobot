using System;

namespace NalogRobot
{
    public class Tax
    {
        public Tax()
        {
            Created = DateTime.Now;
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Document registration number
        /// </summary>
        public string RegNum { get; set; }
        
        /// <summary>
        /// Temporal file name
        /// </summary>
        public string TempFile { get; set; }
        
        /// <summary>
        /// Destination file name
        /// </summary>
        public string DestFile { get; set; }
        
        /// <summary>
        /// Import operation name
        /// </summary>
        public ImportState ImportState { get; set; }
        
        /// <summary>
        /// Last updated state
        /// </summary>
        public DateTime Updated { get; set; }
        
        /// <summary>
        /// Record created date 
        /// </summary>
        public DateTime Created { get; set; }
    }
}