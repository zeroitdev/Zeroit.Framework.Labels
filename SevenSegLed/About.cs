// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="About.cs" company="Zeroit Dev Technologies">
//    This program is for creating Label controls.
//    Copyright ©  2017  Zeroit Dev Technologies
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
//    You can contact me at zeroitdevnet@gmail.com or zeroitdev@outlook.com
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Zeroit.Framework.Labels.LedDisplays
    {

    // *************************************************** class About

    /// <summary>
    /// Class About.
    /// </summary>
    public class About
        {

        // ******************************************* AssemblyCompany

        /// <summary>
        /// Gets the assembly company.
        /// </summary>
        /// <value>The assembly company.</value>
        public string AssemblyCompany
            {
            get
                {
                object[] attributes;
                attributes = Assembly.
                             GetExecutingAssembly ( ).
                             GetCustomAttributes ( 
                                 typeof ( AssemblyCompanyAttribute ), 
                                 false );
                if ( attributes.Length == 0 )
                    {
                    return ( String.Empty );
                    }
                return ( ( ( AssemblyCompanyAttribute ) 
                           attributes [ 0 ] ).Company );
                }
            }

        // ***************************************** AssemblyCopyright

        /// <summary>
        /// Gets the assembly copyright.
        /// </summary>
        /// <value>The assembly copyright.</value>
        public string AssemblyCopyright
            {
            get
                {
                object[] attributes;
                
                attributes = Assembly.
                             GetExecutingAssembly ( ).
                             GetCustomAttributes ( 
                                 typeof ( 
                                     AssemblyCopyrightAttribute ), 
                                 false );
                if ( attributes.Length == 0 )
                    {
                    return ( String.Empty );
                    }
                return ( ( ( AssemblyCopyrightAttribute ) 
                           attributes [ 0 ] ).Copyright );
                }
            }

        // *************************************** AssemblyDescription

        /// <summary>
        /// Gets the assembly description.
        /// </summary>
        /// <value>The assembly description.</value>
        public string AssemblyDescription
            {
            get
                {
                object[] attributes;
                
                attributes = Assembly.
                             GetExecutingAssembly ( ).
                             GetCustomAttributes ( 
                               typeof ( 
                                   AssemblyDescriptionAttribute ), 
                               false );
                if ( attributes.Length == 0 )
                    {
                    return ( String.Empty );
                    }
                return ( ( ( AssemblyDescriptionAttribute ) 
                           attributes [ 0 ] ).Description );
                }
            }

        // ****************************************** AssemblyFilename

        /// <summary>
        /// Gets the assembly filename.
        /// </summary>
        /// <value>The assembly filename.</value>
        public static string AssemblyFilename 
            {
            
            get
                {
                string  filename = String.Empty;
                
                filename = Application.ExecutablePath;
                

                return ( filename );
                }
            }

        // ********************************************** AssemblyPath

        /// <summary>
        /// Gets the assembly path.
        /// </summary>
        /// <value>The assembly path.</value>
        public static string AssemblyPath 
            {
            
            get
                {
                string  path = String.Empty;
                
                path = Path.GetDirectoryName ( AssemblyFilename );

                return ( path );
                }
            }

        // ******************************************* AssemblyProduct

        /// <summary>
        /// Gets the assembly product.
        /// </summary>
        /// <value>The assembly product.</value>
        public string AssemblyProduct
            {
            get
                {
                object[] attributes;
                
                attributes = Assembly.GetExecutingAssembly ( ).
                             GetCustomAttributes ( 
                                typeof ( AssemblyProductAttribute ), 
                                false );
                if ( attributes.Length == 0 )
                    {
                    return ( String.Empty );
                    }
                return ( ( ( AssemblyProductAttribute ) 
                           attributes [ 0 ] ).Product );
                }
            }

        // ********************************************* AssemblyTitle

        /// <summary>
        /// Gets the assembly title.
        /// </summary>
        /// <value>The assembly title.</value>
        public static string AssemblyTitle
            {
            get
                {
                object[] attributes;
                
                attributes = Assembly.
                             GetExecutingAssembly ( ).
                             GetCustomAttributes ( 
                                 typeof ( AssemblyTitleAttribute ), 
                                 false );
                if ( attributes.Length > 0 )
                    {
                    AssemblyTitleAttribute titleAttribute;
                    
                    titleAttribute = 
                        ( AssemblyTitleAttribute ) attributes [ 0 ];
                    if ( ! String.IsNullOrEmpty ( 
                                      titleAttribute.Title ) )
                        {
                        return ( titleAttribute.Title );
                        }
                    }
                return ( Path.GetFileNameWithoutExtension ( 
                                  Assembly.GetExecutingAssembly ( ).
                                           CodeBase ) );
                }
            }

        // ******************************************* AssemblyVersion

        /// <summary>
        /// Gets the assembly version.
        /// </summary>
        /// <value>The assembly version.</value>
        public static string AssemblyVersion
            {
            get
                {
                return ( Assembly.GetExecutingAssembly ( ).
                                  GetName ( ).
                                  Version.ToString ( ) );
                }
            }

        } // class About

    } 
    
