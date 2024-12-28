using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApnaDoctor.Models
{
    public class DoctorSchedule
    {
        [Key]
        public int Id { get; set; }

        public DoctorProfile DoctorProfile { get; set; }
        [ForeignKey("DoctorProfile")]
        public string DoctorProfileId { get; set; }

        public string DayFrom { get; set; }
        public string DayTo { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Week { get; set; }
        public string SlotDuration { get; set; }
    }
    public class DoctorSlotSchedule
    {
        [Key]
        public int Id { get; set; }

        public DoctorSchedule DoctorSchedule { get; set; }
        [ForeignKey("DoctorSchedule")]
        public int DoctorScheduleId { get; set; }
        public DateTime SlotDate { get; set; }
        public string Days { get; set; }
        public string Slot { get; set; }
        public string Status { get; set; }

    }
    public class WeekDays
    {
        [Key]
        public int Id { get; set; }

        

        public string Days { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public string SlotDuration { get; set; }
    }
    public class Slots
    {
        [Key]
        public int Id { get; set; }
        public string SlotDuration { get; set; }
    }

    public class NewSchedule
    {
        public string Days { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Week { get; set; }
        public string SlotDuration { get; set; }
        
    }
}