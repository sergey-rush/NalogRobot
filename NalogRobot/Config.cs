using System.Drawing;

namespace NalogRobot
{
    public class Config
    {
        /// <summary>
        /// Start point
        /// </summary>
        public Point Start = new Point();

        /// <summary>
        /// Preview button point
        /// </summary>
        public Point Preview = new Point();

        /// <summary>
        /// Export button point
        /// </summary>
        public Point Export = new Point();
        
        /// <summary>
        /// Close declaration point
        /// </summary>
        public Point Close = new Point();
        
        /// <summary>
        /// Number of declarations to proceed
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Destination folder to move files on
        /// </summary>
        public string TargetDir { get; set; }
    }
}