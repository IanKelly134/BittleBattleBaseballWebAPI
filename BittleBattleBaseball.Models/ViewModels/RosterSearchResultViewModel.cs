using System.Collections.Generic;
using System.Linq;

namespace BittleBattleBaseball.Models.ViewModels
{
    public class RosterSearchResultViewModel
    {
        private List<HitterPlayerSeasonViewModel> _suggestedLineup;

        private List<PitcherPlayerSeasonViewModel> _suggestedRotation;

        public int Id { get; set; }

        public int Season { get; set; }

        public string TeamName { get; set; }

        public List<PitcherPlayerSeasonViewModel> Pitchers { get; set; }

        public List<HitterPlayerSeasonViewModel> Hitters { get; set; }

        public List<PitcherPlayerSeasonViewModel> SuggestedRotation { 
            get 
            {
                if (_suggestedRotation == null || !_suggestedRotation.Any())
                {
                    if (this.Pitchers != null && this.Pitchers.Any())
                    {
                        _suggestedRotation = Pitchers.OrderByDescending(x => x.Wins).ThenBy(x => x.ERA).ThenBy(x => x.WHIP).Take(5).ToList();
                    }
                }

                return _suggestedRotation;
            } 
        }

        public List<HitterPlayerSeasonViewModel> SuggestedLineup { 
            get 
            {
                if (_suggestedLineup == null || !_suggestedLineup.Any())
                {

                    if (this.Hitters != null && this.Hitters.Any())
                    {
                        var suggestedLineup = new HitterPlayerSeasonViewModel[8];

                        //TODO - ADD INCLUDES
                        HitterPlayerSeasonViewModel catcher = null;
                        HitterPlayerSeasonViewModel firstBase = null;
                        HitterPlayerSeasonViewModel secondBase = null;
                        HitterPlayerSeasonViewModel shortStop = null;
                        HitterPlayerSeasonViewModel thirdBase = null;
                        //HitterPlayerSeasonViewModel leftFielder = null;
                        //HitterPlayerSeasonViewModel centerFielder = null;
                        //HitterPlayerSeasonViewModel rightFielder = null;
                        HitterPlayerSeasonViewModel[] outfielders = new HitterPlayerSeasonViewModel[3];
                        var orderedByBesterHitters = this.Hitters.OrderByDescending(x => x.HR).ThenByDescending(x => x.AVG).ThenByDescending(x => x.OBP).ThenByDescending(x => x.SLG).ThenByDescending(x => x.RBI).ToList();
                        foreach (var hitter in orderedByBesterHitters)
                        {
                            if (hitter.Player.Position == "C" && catcher == null)
                            {
                                catcher = hitter;
                                continue;
                            }
                            else if (hitter.Player.Position == "1B" && firstBase == null)
                            {
                                firstBase = hitter;
                                continue;
                            }
                            else if (hitter.Player.Position == "2B" && secondBase == null)
                            {
                                secondBase = hitter;
                                continue;
                            }
                            else if (hitter.Player.Position == "SS" && shortStop == null)
                            {
                                shortStop = hitter;
                                continue;
                            }
                            else if (hitter.Player.Position == "3B" && thirdBase == null)
                            {
                                thirdBase = hitter;
                                continue;
                            }
                            else if ((hitter.Player.Position == "LF" || hitter.Player.Position == "CF" || hitter.Player.Position == "RF" || hitter.Player.Position == "OF")
                                && (outfielders == null || outfielders[0] == null || outfielders[1] == null || outfielders[2] == null))
                            {
                                if (outfielders[0] == null)
                                {
                                    outfielders[0] = hitter;
                                    continue;
                                }
                                else if (outfielders[1] == null)
                                {
                                    outfielders[1] = hitter;
                                    continue;
                                }
                                else if (outfielders[2] == null)
                                {
                                    outfielders[2] = hitter;
                                    continue;
                                }
                            }
                        }


                        List<HitterPlayerSeasonViewModel> outfieldersSortedBySize;
                        if (outfielders.All(x => x.Player.Position == "OF") || outfielders.Select(x => x.Player.Position).Distinct().Count() != 3)
                        {
                            
                            outfieldersSortedBySize = outfielders.OrderBy(x => x.HR).ToList();
                            outfieldersSortedBySize[0].Player.Position = "CF";
                            outfieldersSortedBySize[1].Player.Position = "LF";
                            outfieldersSortedBySize[2].Player.Position = "RF";
                        }
                        else { 

                            outfieldersSortedBySize = new List<HitterPlayerSeasonViewModel>();
                            outfieldersSortedBySize.AddRange(outfielders);
                        }

                        var unorderedLineup = new List<HitterPlayerSeasonViewModel> { catcher, firstBase, secondBase, shortStop, thirdBase };
                        unorderedLineup.AddRange(outfieldersSortedBySize);

                        if (!unorderedLineup.Any(x => x == null))
                        {

                            var orderedByBestHitter = unorderedLineup.OrderByDescending(x => x.HR).ThenByDescending(x => x.AVG).ThenByDescending(x => x.OBP).ThenByDescending(x => x.SLG).ThenByDescending(x => x.RBI).ToList();


                            //3rd Hitter is best hitter
                            suggestedLineup[2] = orderedByBestHitter[0];
                            orderedByBestHitter.RemoveAt(0);

                            //5th Hitter is next best hitter
                            suggestedLineup[4] = orderedByBestHitter[0];
                            orderedByBestHitter.RemoveAt(0);

                            //Two worst remaining hitters are 7 and 8 hitters
                            suggestedLineup[7] = orderedByBestHitter[orderedByBestHitter.Count - 1];
                            orderedByBestHitter.RemoveAt(orderedByBestHitter.Count - 1);

                            suggestedLineup[6] = orderedByBestHitter[orderedByBestHitter.Count - 1];
                            orderedByBestHitter.RemoveAt(orderedByBestHitter.Count - 1);

                            orderedByBestHitter = orderedByBestHitter.OrderByDescending(x => x.SB).ThenByDescending(x => x.OBP).ToList();
                            //Lead-off Hitter 1 and 2nd are your fastest players left
                            suggestedLineup[0] = orderedByBestHitter[0];
                            orderedByBestHitter.RemoveAt(0);

                            suggestedLineup[1] = orderedByBestHitter[0];
                            orderedByBestHitter.RemoveAt(0);

                            //4th Hitter
                            orderedByBestHitter = orderedByBestHitter.OrderByDescending(x => x.HR).ThenBy(x => x.OBP).ToList();
                            suggestedLineup[3] = orderedByBestHitter[0];
                            orderedByBestHitter.RemoveAt(0);

                            //Add what is left over
                            suggestedLineup[5] = orderedByBestHitter[0];
                            orderedByBestHitter.RemoveAt(0);

                            //suggestedLineup[8] = orderedByBestHitter[0];
                            //orderedByBestHitter.RemoveAt(0);

                            _suggestedLineup = suggestedLineup.ToList();
                        }
                    }
                }

                return _suggestedLineup;
            } 
        }
    }
}
