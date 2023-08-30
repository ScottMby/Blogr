using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogr.Shared
{
    /// <summary>
    /// Contains all of the categories a blog could be
    /// </summary>
    public struct Category
    {
        public List<string> Categories = new List<string>();
        public Category() 
        {
            Categories.Add(Ds);
            Categories.Add(Wd);
            Categories.Add(Ml);
            Categories.Add(Cs);
            Categories.Add(Sa);
            Categories.Add(Db);
        }

        /// <summary>
        /// String for Data Science
        /// </summary>
        string Ds = "Data Science";
        /// <summary>
        /// String for Web Development
        /// </summary>
        string Wd = "Web Development";
        /// <summary>
        /// String for Machine Learning
        /// </summary>
        string Ml = "Machine Learning";
        /// <summary>
        /// String for Cybersecurity
        /// </summary>
        string Cs = "Cybersecurity";
        /// <summary>
        /// String for Software Architecture 
        /// </summary>
        string Sa = "Software Architecture";
        /// <summary>
        /// String for Databases
        /// </summary>
        string Db = "Databases";

    }
}
