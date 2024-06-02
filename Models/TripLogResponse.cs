namespace TravelBuddyApp.Models
{
    public class TripLogResponse
    {
        public int TripLogId { get; set; }
        public int TripId { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public string PhotoPath { get; set; }
    }  
}
