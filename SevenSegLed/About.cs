// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="About.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
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
    
