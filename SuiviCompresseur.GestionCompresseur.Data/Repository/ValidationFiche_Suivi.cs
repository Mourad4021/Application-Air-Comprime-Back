using SuiviCompresseur.GestionCompresseur.Data.Context;
using SuiviCompresseur.GestionCompresseur.Domain.Models;
using System;
using System.Linq;

namespace SuiviCompresseur.GestionCompresseur.Data.Repository
{
    public class ValidationContraint
    {
        private readonly CompresseurDbContext _context;
        public ValidationContraint(CompresseurDbContext context)
        {
            _context = context;
        }
        public string testPost(Fiche_Suivi fiche_Suivi)
        {
            string result;
            int max = 0;
            int maxtothours = 0;
            int maxchargehours = 0;
            int maxIndexDebimetre = 0;
            var maxpossible = _context.Fiche_Suivis.Where(x => x.EquipementFilialeID == fiche_Suivi.EquipementFilialeID).OrderBy(x=>x.Date).LastOrDefault();
            //&&
            //DateTime.Compare(fiche_Suivi.Date, c.Date) < 0).FirstOrDefault();
            if (maxpossible != null)
            {
                max = maxpossible.Index_Electrique;
                maxtothours = maxpossible.Nbre_Heurs_Total;
                maxchargehours = maxpossible.Nbre_Heurs_Charge;
                maxIndexDebimetre = maxpossible.Index_Debitmetre;
            }
            string datedouble = TestDoubleDate(fiche_Suivi);
            int firtM = 0;
            if (datedouble == "true")
            {
                int value = DateTime.Compare(fiche_Suivi.Date, DateTime.Now);
                if (value <= 0)
                {

                    ////if (fiche_Suivi.Date.DayOfWeek != DayOfWeek.Saturday && fiche_Suivi.Date.DayOfWeek != DayOfWeek.Sunday)
                    // //{
                    var exist = _context.Fiche_Suivis.Where(c => c.EquipementFilialeID == fiche_Suivi.EquipementFilialeID).FirstOrDefault();
                    if (exist != null)
                    {
                        var FirstMonth = _context.Fiche_Suivis.Min(c => c.Date);
                        firtM = DateTime.Compare(fiche_Suivi.Date, FirstMonth);
                        if (firtM != 0)
                        {
                            DateTime lastMonthDate;
                            int annee = fiche_Suivi.Date.Year;
                            int mois = fiche_Suivi.Date.Month - 1;
                            if (mois == 0)
                            {
                                mois = 12;
                                annee--;
                            }
                            int numberOfDays = DateTime.DaysInMonth(annee, mois);
                            DateTime date = new DateTime(annee, mois, numberOfDays);

                            if (date.DayOfWeek == DayOfWeek.Saturday)
                            {
                                lastMonthDate = new DateTime(annee, mois, numberOfDays - 1);
                            }
                            else if (date.DayOfWeek == DayOfWeek.Sunday)
                            {
                                lastMonthDate = new DateTime(annee, mois, numberOfDays - 2);
                            }
                            else
                            {
                                lastMonthDate = new DateTime(annee, mois, numberOfDays);
                            }

                            if (_context.Fiche_Suivis.Count(c => c.EquipementFilialeID == fiche_Suivi.EquipementFilialeID) < 30)
                            {
                                result = "true";
                            }
                            else
                            {
                                var res = _context.Fiche_Suivis.Where(c => c.Date == lastMonthDate).FirstOrDefault();
                                if (res != null)
                                    result = "true";
                                else
                                    result = "The last day of the previous month not completed";
                            }

                        }
                        else
                            result = "true";
                    }
                    else
                        result = "true";

                    //}
                    //else
                    //    result = "Week-end";
                }
                else
                    result = "Date superior to the date of today";

                if (result == "true")
                {
                    if (fiche_Suivi.Nbre_Heurs_Charge < fiche_Suivi.Nbre_Heurs_Total)
                    {



                        if (fiche_Suivi.Index_Electrique >= max)
                        {
                            if (fiche_Suivi.Nbre_Heurs_Total >= maxtothours)
                            {
                                if (fiche_Suivi.Nbre_Heurs_Charge >= maxchargehours)
                                {
                                    if (fiche_Suivi.Index_Debitmetre >= maxIndexDebimetre)
                                    {
                                        return "true";
                                    }
                                    else
                                    {
                                        return "Index debimetre lower than the previous index";
                                    }
                                }
                                else
                                {
                                    return "nbre heures charge lower than the previous nbre heures charge";
                                }
                            }
                            else
                            {
                                return "nbre heures total lower than the previous nbre heures total";
                            }
                        }
                        else
                            return "Index lower than the previous index";
                    }


                    else
                        return "Total number of hours less than the number of hours in charge";
                }
                else
                    return result;
            }
            else
                return datedouble;
        }

        public string testPut(Fiche_Suivi fiche_Suivi, Guid id)
        {
            int max = 0;
            int maxtothours = 0;
            int maxchargehours = 0;
            int maxIndexDebimetre = 0;
            var maxpossible = _context.Fiche_Suivis.Where(x => x.EquipementFilialeID == fiche_Suivi.EquipementFilialeID).OrderBy(x => x.Date).LastOrDefault();
            //&&
            //DateTime.Compare(fiche_Suivi.Date, c.Date) < 0).FirstOrDefault();
            if (maxpossible != null)
            {
                max = maxpossible.Index_Electrique;
                maxtothours = maxpossible.Nbre_Heurs_Total;
                maxchargehours = maxpossible.Nbre_Heurs_Charge;
                maxIndexDebimetre = maxpossible.Index_Debitmetre;
            }
            string result;
            //string datedouble = TestDoubleDatePut(fiche_Suivi, id);
            //if (datedouble == "true")
            //{


            //int value = DateTime.Compare(fiche_Suivi.Date, DateTime.Now);
            //if (value <= 0)
            //{

            //if (fiche_Suivi.Date.DayOfWeek != DayOfWeek.Saturday && fiche_Suivi.Date.DayOfWeek != DayOfWeek.Sunday)

            //{

            //    DateTime date2;
            //    int annee = fiche_Suivi.Date.Year;
            //    int mois = fiche_Suivi.Date.Month - 1;
            //    if (mois == 0)
            //    {
            //        mois = 12;
            //        annee--;
            //    }
            //    int last = DateTime.DaysInMonth(annee, mois);
            //    DateTime date = new DateTime(annee, mois, last);

            //    if (date.DayOfWeek == DayOfWeek.Saturday)
            //    {
            //        date2 = new DateTime(annee, mois, last - 1);
            //    }
            //    else if (date.DayOfWeek == DayOfWeek.Sunday)
            //    {
            //        date2 = new DateTime(annee, mois, last - 2);
            //    }
            //    else
            //    {
            //        date2 = new DateTime(annee, mois, last);
            //    }
            //    var res = _context.Fiche_Suivis.Where(c => c.Date == date2).FirstOrDefault();
            //    if (res != null)
            //        result = "true";
            //    else
            //        result = "The last day of the previous month not completed";

            //}
            //    else
            //        result = "Week-end";
            //}
            //else
            //    result = "Date superior to the date of today";


            //if (result == "true")
            //{
         //   int max = _context.Fiche_Suivis.Where(c => (c.EquipementFilialeID == fiche_Suivi.EquipementFilialeID) && (c.Date < fiche_Suivi.Date)).Max(c => c.Index_Electrique);
            if (fiche_Suivi.Nbre_Heurs_Charge < fiche_Suivi.Nbre_Heurs_Total)
            {
                if (fiche_Suivi.Index_Electrique >= max)
                {
                    if (fiche_Suivi.Nbre_Heurs_Total >= maxtothours)
                    {
                        if (fiche_Suivi.Nbre_Heurs_Charge >= maxchargehours)
                        {
                            if (fiche_Suivi.Index_Debitmetre >= maxIndexDebimetre)
                            {
                                var entity = _context.Fiche_Suivis.Find(id);
                                if (entity != null)
                                {
                                    return "true";
                                }
                                else
                                {
                                    return "Fiche suivi don't exist";
                                }

                            }
                            else
                            {
                                return "Index debimetre lower than the previous index";
                            }
                        }
                        else
                        {
                            return "nbre heures charge lower than the previous nbre heures charge";
                        }
                    }
                    else
                    {
                        return "nbre heures total lower than the previous nbre heures total";
                    }
                }
                else
                    return "Index lower than the previous index";
            }
            else
                return "Total number of hours less than the number of hours in charge";
            //}
            //else
            //return result;
            //}
            //else return datedouble;
        }




        public string TestDoubleDate(Fiche_Suivi fiche_Suivi)
        {
            var doubledate = _context.Fiche_Suivis.Where(c => c.EquipementFilialeID == fiche_Suivi.EquipementFilialeID && c.Date == fiche_Suivi.Date).FirstOrDefault();
            if (doubledate == null)
                return "true";
            else
                return "Existing Fiche_suivi at this date";
        }

        public string TestDoubleDatePut(Fiche_Suivi fiche_Suivi, Guid id)
        {
            var entity = _context.Fiche_Suivis.Find(id);
            if (entity.Date == fiche_Suivi.Date)
            {
                return "true";
            }
            else
            {
                var doubledate = _context.Fiche_Suivis.Where(c => c.EquipementFilialeID == fiche_Suivi.EquipementFilialeID && c.Date == fiche_Suivi.Date).FirstOrDefault();
                if (doubledate == null)
                    return "true";
                else
                    return "Existing Fiche_suivi at this date";
            }

        }

    }


}