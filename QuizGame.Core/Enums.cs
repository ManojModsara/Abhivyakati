using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Core
{
    public static class Enums
    {
        public enum AppraisalMarking
        {
            Productivity = 1,
            Communication = 2,
            LeaderShip = 3,
            PersonalDevelopment = 4,
            RelationShips = 5,
            Management = 6
        }

        public enum UserRoles
        {
            DV = 1,
            SD = 2,
            TL = 3,
            PM = 4,
            HR = 5,
            QA = 6,
            PMO = 7,
            DG = 8,
            BA = 9,
            TLS = 10,
            SEO = 11,
            OP = 12,
            DEO = 13,
            UKPM = 14
        }
        // # Added by Pooja #
        public enum ModalSize
        {
            Small,
            Large,
            Medium,
            XLarge
        }
        public enum ActivityStatus
        {
            Away = 1,
            Working = 2,
            Free = 3,
            NotLoggedIn = 4,
            Leave = 5
        }

        public enum LeaveStatus
        {
            Pending = 6,
            Approved = 7,
            Cancelled = 8,
            UnApproved = 9
        }

        public enum LeaveType
        {
            Normal = 15,
            Urgent = 16,
            Adjust = 17,
            Auto = 18
        }

        public enum ProjectDevWorkStatus
        {
            Closed = 11,
            Running = 12
        }
        public enum ProjectDevWorkRole
        {
            Paid = 13,
            Addon = 14
        }

        public enum IntwQuestype
        {
            MultipleChoice = 1,
            SingleChoice = 2
        }

        public enum OwnersTypes
        {
            Estimate = 1,
            Communication = 2,
            Technical = 3,
            Technology = 4
        }

        public enum ChasingStatus
        {
            // Chase = 1,
            // Chased = 2,            
            // AlmostConverted=3,
            Pending = 1,
            Converted = 2,
        }

        public enum ChasingType
        {
            IsExistingLead = 1,
            IsExistingClient = 2,
            IsNewClient = 3
        }

        public enum TaskStatus
        {
            Pending = 1,
            Assigned = 2,
            Completed = 3
        }
        public enum MessageType
        {
            Warning,
            Success,
            Danger,
            Info
        }
        public enum Priority
        {
            VeryHigh = 1,
            High = 2,
            Medium = 3,
            Low = 4
        }

        // End
        public static bool CompareDate(this DateTime? dateTime, DateTime? otherDate)
        {
            if (dateTime.HasValue && otherDate.HasValue)
            {
                return dateTime.Value.Day == otherDate.Value.Day
                       && dateTime.Value.Month == otherDate.Value.Month
                       && dateTime.Value.Year == otherDate.Value.Year;
            }

            return false;

        }

        // Enums for bucket model
        public enum BucketModel
        {


        }

        public enum UserTimeSheet
        {
            //Local

            // AdditionalFree=18,



            //Live

            AdditionalFree = 15,
            FreeBugFixing = 35,
            Other = 145

        }
        public enum ProjectGroup
        {

            //local
            //DotNetDevloper = 3,
            //PHPDeveloper = 5,
            //MobileDevloper = 6,
            //QualityAnalyst = 7,
            //Designer = 8,
            //BusinessAnalyst=9
            // ProjectManager=9,

            //Live
            DotNetDevloper = 3,
            PHPDeveloper = 5,
            MobileDevloper = 6,
            QualityAnalyst = 7,
            Designer = 8,
            ProjectManager = 9,
            BusinessAnalyst = 10

        }

        public enum KnowledgeType
        {
            NewConcept = 1,
            ExistingComponent = 2
        }

        //Project Closer
        public enum CloserType
        {
            Pending = 1,
            DeadResponse = 3,
            Converted = 4
        }
        public enum CRMStatus
        {
            Completed = 1,
            [Display(Name = "On Hold")]
            OnHold = 2
        }
        public enum ProjectInvoiceStatus
        {
            [Display(Name = "Pending")]
            Pending = 1,
            [Display(Name = "Partial Payment")]
            PartialPayment = 2,
            [Display(Name = "Paid")]
            Paid = 3,
            Cancelled = 4,
            WaitingApproval = 5,
            BadDebt = 6
        }

        public enum ClientQualtiy
        {
            Average = 1,
            Promising = 2,
            Poor = 3
        }

        public enum ReviewRoleType
        {
            [Display(Name = "Developer")]
            Developer = 1,
            [Display(Name = "Team Leader")]
            TeamLeader = 2,
        }
        public enum ReviewSkillType
        {
            [Display(Name = "Hard Skill")]
            HardSkill = 1,
            [Display(Name = "Soft Skill")]
            SoftSkill = 2,
        }


        public enum PerformanceReviewOptions
        {
            Unexpected = 1,
            [Display(Name = "Meet Expectations")]
            Meet_Expectations = 2,
            [Display(Name = "Exceed Expectations")]
            Exceed_Expectations = 3,
            Outstanding = 4
        }

        public enum BloodGroups
        {
            [Display(Name = "O+")]
            Opos = 1,
            [Display(Name = "O-")]
            Oneg = 2,
            [Display(Name = "A+")]
            Apos = 3,
            [Display(Name = "A-")]
            Aneg = 4,
            [Display(Name = "B+")]
            Bpos = 5,
            [Display(Name = "B-")]
            Bneg = 6,
            [Display(Name = "AB+")]
            ABpos = 7,
            [Display(Name = "AB-")]
            ABneg = 8
        }
        public enum InterViewStaus
        {
            Pending = 0,
            Selected = 1,
            Rejected = 2,
            OnHold = 3
        }



    }
}
