using Atypical.Crosscutting.Dtos.Diary;
using System.Collections.Generic;
using Atypical.Web.Models.Diary;

namespace Atypical.Web.Helpers
{
    public static class ChartHelper
    {
        // Helpful methods forthe chart controller
        public static ChartViewModel TurnChartToModel(FullChartDto chartDto)
        {
            // Turn the weeklyChart into something for the model
            ChartViewModel chartModel = new ChartViewModel()
            {
                Type = chartDto.Type,
                Title = chartDto.Title,
                Nodes = new List<ChartNodeViewModel>()
            };

            // turn all the nodes in the dto into view models and add them
            foreach (var node in chartDto.Nodes)
            {
                ChartNodeViewModel newNode = new ChartNodeViewModel()
                {
                    NodeName = node.NodeName,
                    Happy = node.Happy,
                    Sad = node.Sad,
                    Confident = node.Confident,
                    Mad = node.Mad,
                    Hopeful = node.Hopeful,
                    Scared = node.Scared
                };

                chartModel.Nodes.Add(newNode);
            }

            return chartModel;
        }
    }

}