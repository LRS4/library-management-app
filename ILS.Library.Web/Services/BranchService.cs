using ILS.Library.DataAccess.SecurityDb.Entities;
using ILS.Library.DataAccess.SecurityDb.Entities.Asset;
using ILS.Library.DataAccess.SecurityDb.Entities.Branch;
using ILS.Library.DataAccess.SecurityDb.Entities.Users;
using ILS.Library.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ILS.Library.Web.Services
{
    public class BranchService : IBranchService
    {
        #region Private properties
        private readonly ILSContext _context;

        #endregion

        #region Constructor
        public BranchService(ILSContext context)
        {
            _context = context;
        }

        #endregion

        #region BranchService
        public void Add(BranchDetails newBranch)
        {
            _context.Add(newBranch);
            _context.SaveChanges();
        }

        public BranchDetails Get(int branchId)
        {
            return GetAll().FirstOrDefault(b => b.BranchId == branchId);
        }

        public IEnumerable<BranchDetails> GetAll()
        {
            return _context.BranchDetails
                .Include(b => b.Patron)
                .Include(b => b.LibraryAsset);
        }

        public IEnumerable<LibraryAsset> GetAssets(int branchId)
        {
            return _context.BranchDetails
                .Include(b => b.LibraryAsset)
                .FirstOrDefault(b => b.BranchId == branchId)
                .LibraryAsset;
        }

        public IEnumerable<string> GetBranchHours(int branchId)
        {
            var hours = _context.BranchHours.Where(h => h.Branch.BranchId == branchId);
            return MakeBusinessHoursReadable(hours);
        }

        public IEnumerable<Patron> GetPatrons(int branchId)
        {
            return _context.BranchDetails
                .Include(b => b.Patron)
                .FirstOrDefault(b => b.BranchId == branchId)
                .Patron;
        }

        public bool IsBranchOpen(int branchId)
        {
            var currentTimeHour = DateTime.Now.Hour;
            var currentDayOfWeek = (int)DateTime.Now.DayOfWeek + 1;
            var hours = _context.BranchHours.Where(h => h.Branch.BranchId == branchId);
            var daysHours = hours.FirstOrDefault(h => h.DayOfWeek == currentDayOfWeek);
            
            return currentTimeHour < daysHours.CloseTime && currentTimeHour > daysHours.OpenTime;
        }
        #endregion

        #region Private methods
        private static IEnumerable<string> MakeBusinessHoursReadable(IEnumerable<BranchHours> branchHours)
        {
            var hours = new List<string>();

            foreach(var time in branchHours)
            {
                var day = ReadableDay(time.DayOfWeek);
                var openTime = ReadableTime(time.OpenTime);
                var closeTime = ReadableTime(time.CloseTime);
                var timeEntry = $"{day} {openTime} to {closeTime}";
                hours.Add(timeEntry);
            }

            return hours;
        }

        private static string ReadableDay(int dayOfWeek)
        {
            // our data correlates 1 to Sunday, so subtract 1
            return Enum.GetName(typeof(DayOfWeek), dayOfWeek - 1);
        }

        private static string ReadableTime(int time)
        {
            return TimeSpan.FromHours(time).ToString("hh':'mm");
        }

        #endregion
    }
}
