﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EpiPlanTool.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("EpiPlanTool.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to WITH 
        ///RCTR_STATUS AS (
        ///  SELECT 
        ///    REACTOR_ID,
        ///    REACT_TYPE,
        ///    MAX(CASE WHEN CHAMBER=&apos;A&apos; THEN STATUS ELSE NULL END) A,
        ///    MAX(CASE WHEN CHAMBER=&apos;B&apos; THEN STATUS ELSE NULL END) B,
        ///    MAX(CASE WHEN CHAMBER=&apos;C&apos; THEN STATUS ELSE NULL END) C
        ///  FROM (
        ///    SELECT 
        ///      TO_NUMBER(REGEXP_REPLACE(ENTITY_DESCRIPTION, &apos;[^0-9]+&apos;, &apos;&apos;)) REACTOR_ID,
        ///      NVL(REGEXP_SUBSTR(ENTITY_DESCRIPTION, &apos;(CHAMBER\s)([ABC]{1})&apos;, 1,1,&apos;i&apos;,2) ,&apos;A&apos;) CHAMBER,
        ///      ENTITY_STATUS_1 STATUS,
        ///      ENTITY_TYPE REACT_TYPE
        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GET_REACTOR_STATUSES {
            get {
                return ResourceManager.GetString("GET_REACTOR_STATUSES", resourceCulture);
            }
        }
    }
}
