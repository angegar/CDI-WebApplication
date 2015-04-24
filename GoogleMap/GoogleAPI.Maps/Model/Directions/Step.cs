using System.Runtime.Serialization;

namespace GoogleAPI.Maps.Model.Directions {
    /// <summary>
    /// Each element in the steps array defines a single step of the calculated directions. 
    /// A step is the most atomic unit of a direction's route, containing a single step describing 
    /// a specific, single instruction on the journey. E.g. "Turn left at W. 4th St." The step not 
    /// only describes the instruction but also contains distance and duration information relating 
    /// to how this step relates to the following step. For example, a step denoted as 
    /// "Merge onto I-80 West" may contain a duration of "37 miles" and "40 minutes," 
    /// indicating that the next step is 37 miles/40 minutes from this step.
    /// </summary>
    [DataContract]
    public class Step {
        /// <summary>
        /// Contains the distance covered by this step until the next step.
        /// </summary>
        [DataMember(Name="distance")]
        public TextValue Distance { get; set; }
        /// <summary>
        /// Contains the typical time required to perform the step, until the next step.
        /// </summary>
        [DataMember(Name = "duration")]
        public TextValue Duration { get; set; }
        /// <summary>
        /// Contains the location of the starting point of this step, as a single set of lat and lng fields.
        /// </summary>
        [DataMember(Name = "end_location")]
        public CardinalDirection EndLocation { get; set; }
        /// <summary>
        /// Contains the location of the starting point of this step, as a single set of lat and lng fields.
        /// </summary>
        [DataMember(Name = "start_location")]
        public CardinalDirection StartLocation { get; set; }
        /// <summary>
        /// Contains formatted instructions for this step, presented as an HTML text string.
        /// </summary>
        [DataMember(Name = "html_instructions")]
        public string HtmlInstructions { get; set; }

        [DataMember(Name = "polyline")]
        public Polyline Polyline { get; set; }

        [DataMember(Name = "travel_mode")]
        public string TravelMode { get; set; }
    }
}
