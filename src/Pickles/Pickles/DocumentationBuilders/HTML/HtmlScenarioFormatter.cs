﻿#region License

/*
    Copyright [2011] [Jeffrey Cameron]

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

#endregion

using System.Linq;
using System.Xml.Linq;
using Pickles.Parser;

namespace Pickles.DocumentationBuilders.HTML
{
    public class HtmlScenarioFormatter
    {
        private readonly HtmlDescriptionFormatter htmlDescriptionFormatter;
        private readonly HtmlImageResultFormatter htmlImageResultFormatter;
        private readonly HtmlStepFormatter htmlStepFormatter;
        private readonly XNamespace xmlns;

        public HtmlScenarioFormatter(
            HtmlStepFormatter htmlStepFormatter,
            HtmlDescriptionFormatter htmlDescriptionFormatter,
            HtmlImageResultFormatter htmlImageResultFormatter)
        {
            this.htmlStepFormatter = htmlStepFormatter;
            this.htmlDescriptionFormatter = htmlDescriptionFormatter;
            this.htmlImageResultFormatter = htmlImageResultFormatter;
            xmlns = HtmlNamespace.Xhtml;
        }

        public XElement Format(Scenario scenario, int id)
        {
            return new XElement(xmlns + "li",
                                new XAttribute("class", "scenario"),
                                htmlImageResultFormatter.Format(scenario),
                                new XElement(xmlns + "div",
                                             new XAttribute("class", "scenario-heading"),
                                             new XElement(xmlns + "h2", scenario.Name),
                                             htmlDescriptionFormatter.Format(scenario.Description)
                                    ),
                                new XElement(xmlns + "div",
                                             new XAttribute("class", "steps"),
                                             new XElement(xmlns + "ul",
                                                          scenario.Steps.Select(step => htmlStepFormatter.Format(step)))
                                    )
                );
        }
    }
}