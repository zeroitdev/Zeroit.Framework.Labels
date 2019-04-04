// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-28-2018
// ***********************************************************************
// <copyright file="LedMatrixControl.cs" company="Zeroit Dev Technologies">
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing.Drawing2D; 

namespace Zeroit.Framework.Labels.LedDisplays
{
    //-----------------------------------------------------//
    //                    ENUMS                            //
    //-----------------------------------------------------//

    /// <summary>
    /// Define the direction of a displayed item
    /// </summary>
    public enum ItemDirection : int
  {
        /// <summary>
        /// Sets the direction to Up
        /// </summary>
        Up = 0,
        /// <summary>
        /// Sets the direction to Down
        /// </summary>
        Down = 2,
        /// <summary>
        /// Sets the direction to Left
        /// </summary>
        Left = 3,
        /// <summary>
        /// Sets the direction to Right
        /// </summary>
        Right = 4 
  }

    /// <summary>
    /// Define the speed of a displayed item
    /// </summary>
    public enum ItemSpeed : int
    {
        /// <summary>
        /// Sets the speed of the displayed item to Slow
        /// </summary>
        Slow = 0,
        /// <summary>
        /// Sets the speed of the displayed item to Idle
        /// </summary>
        Idle = 1,
        /// <summary>
        /// Sets the speed of the displayed item to Fast
        /// </summary>
        Fast = 2
    }

    /// <summary>
    /// Define the shape of a led
    /// </summary>
    public enum LedSyle : int
  {
        /// <summary>
        /// Sets the shape to Led
        /// </summary>
        Round = 0,
        /// <summary>
        /// Sets the shape to Led
        /// </summary>
        Square = 1 
  }


    //-----------------------------------------------------//
    //                   STRUCTS                           //
    //-----------------------------------------------------//

    /// <summary>
    /// Define the composition of a symbol line from the xml "LedLine" node
    /// </summary>
    struct LedOnLine
  {
        /// <summary>
        /// The i line no
        /// </summary>
        public int    iLineNo;///<!-- Line number in the symbol composition-->
                          /// <summary>
                          /// The s led on
                          /// </summary>
        public string sLedOn; ///<!-- String which represents the led On -->

        /// <summary>
        /// Structure constructor
        /// </summary>
        /// <param name="p_iLineNo">Line number</param>
        /// <param name="p_sLedOn">Line string code</param>
        public LedOnLine(int    p_iLineNo,
                     string p_sLedOn)
    {
      iLineNo = p_iLineNo;
      sLedOn  = p_sLedOn;
    }
  }

    /// <summary>
    /// Define the composition of a symbol
    /// </summary>
    struct LedMatrixSymbol
  {
        /// <summary>
        /// The b led on matrix
        /// </summary>
        public bool[,] bLedOnMatrix;///<!-- The led On definition         -->
                                /// <summary>
                                /// The s description
                                /// </summary>
        public string  sDescription;///<!-- The description of the symbol -->
                                /// <summary>
                                /// The UI code
                                /// </summary>
        public uint    uiCode;      ///<!-- The code of the symbol        -->


        /// <summary>
        /// Structure constructor
        /// </summary>
        /// <param name="p_uiCode">Code of the symbol</param>
        /// <param name="p_sDescription">Description of the symbol</param>
        /// <param name="p_bLedOnMatrix">Led On definition</param>
        public LedMatrixSymbol(uint    p_uiCode,
                           string  p_sDescription,
                           bool[,] p_bLedOnMatrix)
    {
      uiCode       = p_uiCode;
      sDescription = p_sDescription;
      bLedOnMatrix = p_bLedOnMatrix;
    }
  }

    /// <summary>
    /// Define a whole symbols font
    /// </summary>
    struct LedMatrixSymbolFont
  {
        /// <summary>
        /// The s name
        /// </summary>
        public string                sName;        ///<!-- The font name                  -->
                                               /// <summary>
                                               /// The LST symbol list
                                               /// </summary>
        public List<LedMatrixSymbol> lstSymbolList;///<!-- The list of symbols in the font -->

        /// <summary>
        /// Structure constructor
        /// </summary>
        /// <param name="p_sName">Font name</param>
        /// <param name="p_lstSymbolList">List of symbols in the font</param>
        public LedMatrixSymbolFont(string p_sName,
                               List<LedMatrixSymbol> p_lstSymbolList)
    {
      sName         = p_sName;
      lstSymbolList = p_lstSymbolList;
    }
  }

    /// <summary>
    /// Define a collection of symbol fonts
    /// </summary>
    struct LedMatrixSymbolFontCollection
  {
        /// <summary>
        /// The LST font list
        /// </summary>
        public List<LedMatrixSymbolFont> lstFontList;///<!-- The list of fonts -->

        /// <summary>
        /// Structure constructor
        /// </summary>
        /// <param name="p_lstFontList">List of fonts</param>
        public LedMatrixSymbolFontCollection(List<LedMatrixSymbolFont> p_lstFontList)
    {
      lstFontList = p_lstFontList;
    }
  }


    //-----------------------------------------------------//
    //                     CLASS                           //
    //-----------------------------------------------------//

    /// <summary>
    /// Definition of the "physical" properties of a led in the matrix
    /// </summary>
    class LedProperty
  {
        /// <summary>
        /// The m pt location
        /// </summary>
        public Point m_ptLocation;///<!-- Position of the led in the matrix (center of the led)-->
                              /// <summary>
                              /// The m i radius
                              /// </summary>
        public int   m_iRadius;   ///<!-- Radius of the led                                    -->

        /// <summary>
        /// Constructor
        /// </summary>
        public LedProperty()
    {
      // Initialisations
      m_ptLocation.X = 0;
      m_ptLocation.Y = 0;
      m_iRadius      = 0;
    }
  }

    /// <summary>
    /// Definition of a item displayed on the led matrix
    /// </summary>
    [Serializable]
    public class LedMatrixItem
  {
        /// <summary>
        /// The m identifier
        /// </summary>
        int m_Id;              ///<!-- Id               -->
                                     /// <summary>
                                     /// The m b master led on
                                     /// </summary>
        bool[,]       m_bMasterLedOn;    ///<!-- Led On definiton -->
                                     /// <summary>
                                     /// The m p current location
                                     /// </summary>
        Point m_pCurrentLocation;///<!-- Current location -->
                                     /// <summary>
                                     /// The m d direction
                                     /// </summary>
        ItemDirection m_dDirection;      ///<!-- Direction        -->
                                     /// <summary>
                                     /// The m s speed
                                     /// </summary>
        ItemSpeed m_sSpeed;          ///<!-- Speed            -->


        /// <summary>
        /// Basic constructor
        /// </summary>
        public LedMatrixItem()
    {
      // Initialisations
      m_Id                 = -1;
      m_pCurrentLocation.X =  0;
      m_pCurrentLocation.Y =  0;
    }

        /// <summary>
        /// Evolved constructor
        /// </summary>
        /// <param name="p_Id">Id code</param>
        /// <param name="p_bMasterLedOn">Led On definition</param>
        /// <param name="p_pInitalLocation">Initial location</param>
        /// <param name="p_dDirection">Direction</param>
        /// <param name="p_sSpeed">Speed</param>
        public LedMatrixItem(int           p_Id,
                         bool[,]       p_bMasterLedOn,
                         Point         p_pInitalLocation,
                         ItemDirection p_dDirection,
                         ItemSpeed     p_sSpeed)
    {
      // Initialisations
      m_Id               = p_Id;
      m_bMasterLedOn     = p_bMasterLedOn;
      m_dDirection       = p_dDirection;
      m_sSpeed           = p_sSpeed;
      m_pCurrentLocation = p_pInitalLocation;
    }

        /// <summary>
        /// Get the Id of the item
        /// </summary>
        /// <returns>Id of the item</returns>
        public int GetId()
    {
      return m_Id;
    }

        /// <summary>
        /// Get the current line offset of the item (Y pos)
        /// </summary>
        /// <returns>Current line offset of the item</returns>
        public int GetLineOffset()
    {
      return m_pCurrentLocation.Y;
    }

        /// <summary>
        /// Get the current row offset of the item (X pos)
        /// </summary>
        /// <returns>Current row offset of the item</returns>
        public int GetRowOffset()
    {
      return m_pCurrentLocation.X;
    }

        /// <summary>
        /// Get the led On definition of the item
        /// </summary>
        /// <returns>Led On definition of the item</returns>
        public bool[,] GetMasterLedOn()
    {
      return m_bMasterLedOn;
    }

        /// <summary>
        /// Define the led On definition of the item
        /// </summary>
        /// <param name="p_bMasterLedOn">Led On definition</param>
        public void SetLedOnArray(bool[,] p_bMasterLedOn)
    {
      m_bMasterLedOn = p_bMasterLedOn;
    }

        /// <summary>
        /// Define the current location of the item
        /// </summary>
        /// <param name="p_pLocation">Location of the item</param>
        public void SetLocation(Point p_pLocation)
    {
      m_pCurrentLocation = p_pLocation;
    }

        /// <summary>
        /// Define the direction of the item
        /// </summary>
        /// <param name="p_dDirection">Direction of the item</param>
        public void SetDirection(ItemDirection p_dDirection)
    {
      m_dDirection = p_dDirection;
    }

        /// <summary>
        /// Define the speed of the item
        /// </summary>
        /// <param name="p_sSpeed">Speed of the item</param>
        public void SetSpeed(ItemSpeed p_sSpeed)
    {
      m_sSpeed = p_sSpeed;
    }

        /// <summary>
        /// Move the item in the led matrix according to its properties
        /// </summary>
        /// <param name="p_uiTickCount">Clock tick count</param>
        /// <param name="p_iNbCtrlLedLine">Number of lines in the matrix</param>
        /// <param name="p_iNbCtrLedRow">Number of rows in the matrix</param>
        public void MoveItem(uint p_uiTickCount,int p_iNbCtrlLedLine,int p_iNbCtrLedRow)
    {
      int iOffset = 0;

      //---------------------------------------------------
      // Offset computations

      // Define the offset amplitude
      switch (m_sSpeed)
      {
        case ItemSpeed.Slow:
          {
            if ((p_uiTickCount % 2) == 0)
            {
              iOffset = 1;
            }
            break;
          }
        case ItemSpeed.Idle:
          {
            iOffset = 1;
            break;
          }
        case ItemSpeed.Fast:
          {
            iOffset = 2;
            break;
          }

        default:
          break;
      }

      // Any movement ?
      if (iOffset == 0)
      {
        return;
      }

      // Applies the offest according to the item direction
      switch (m_dDirection)
      {
        case ItemDirection.Up:
          m_pCurrentLocation.Y -= iOffset;
          break;
        case ItemDirection.Down:
          m_pCurrentLocation.Y += iOffset;
          break;
        case ItemDirection.Left:
          m_pCurrentLocation.X -= iOffset;
          break;
        case ItemDirection.Right:
          m_pCurrentLocation.X += iOffset;
          break;
        default:
          break;
      }


      //---------------------------------------------------
      // Corrections

      // Exit by right
      if (m_pCurrentLocation.X >= p_iNbCtrLedRow)
      {
        m_pCurrentLocation.X = -m_bMasterLedOn.GetLength(1);
      }
      // Exit by left
      else if (m_pCurrentLocation.X<-m_bMasterLedOn.GetLength(1))
      {
        m_pCurrentLocation.X = p_iNbCtrLedRow - 1;
      }

      // Exit by bottom
      if (m_pCurrentLocation.Y >= p_iNbCtrlLedLine)
      {
        m_pCurrentLocation.Y = -m_bMasterLedOn.GetLength(0);
      }
      // Exit by top
      else if (m_pCurrentLocation.Y < -m_bMasterLedOn.GetLength(0))
      {
        m_pCurrentLocation.Y = p_iNbCtrlLedLine - 1;
      }
    }

  }

    /// <summary>
    /// Definition of the led matrix control
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    public partial class ZeroitMatrixLabel : Control
  {
        /// <summary>
        /// The m i nb led lines
        /// </summary>
        int m_iNbLedLines;      ///<!-- Number of led lines                                         --> 
                                                      /// <summary>
                                                      /// The m i nb led rows
                                                      /// </summary>
        int m_iNbLedRows;       ///<!-- Number of led rows                                          -->
                                                      /// <summary>
                                                      /// The m d led size coeff
                                                      /// </summary>
        double m_dLedSizeCoeff;    ///<!-- Coefficent of the led diameter vs the led cell size: (0->1) --> 
                                                      /// <summary>
                                                      /// The m b on brush
                                                      /// </summary>
        SolidBrush m_bOnBrush;         ///<!-- Led On brush                                                -->
                                                      /// <summary>
                                                      /// The m b off brush
                                                      /// </summary>
        SolidBrush m_bOffBrush;        ///<!-- Led Off brush                                               -->
                                                      /// <summary>
                                                      /// The m ls led style
                                                      /// </summary>
        LedSyle m_lsLedStyle;       ///<!-- Led style                                                   -->
                                                      /// <summary>
                                                      /// The m ll led property list
                                                      /// </summary>
        List<List<LedProperty>>       m_llLedPropertyList;///<!-- List of the matrix led                                      -->
                                                      /// <summary>
                                                      /// The m fc font collection
                                                      /// </summary>
        LedMatrixSymbolFontCollection m_fcFontCollection; ///<!-- Font collection of the control                              -->
                                                      /// <summary>
                                                      /// The m l item list
                                                      /// </summary>
        List<LedMatrixItem>           m_lItemList;        ///<!-- List of items displayed on the control                      -->
                                                      /// <summary>
                                                      /// The m i item identifier count
                                                      /// </summary>
        int m_iItemIdCount;     ///<!-- Item Id counter                                             -->
                                                      /// <summary>
                                                      /// The m t timer
                                                      /// </summary>
        Timer m_tTimer;           ///<!-- Internal control timer                                      -->
                                                      /// <summary>
                                                      /// The m UI tick count
                                                      /// </summary>
        uint m_uiTickCount;      ///<!-- Timer ticks counting                                        -->

        /// <summary>
        /// The set text
        /// </summary>
        string setText = "Initial Item";
        /// <summary>
        /// The identifier
        /// </summary>
        int id = 0;
        //-------------------------------//
        //         CONSTRUCTOR           //
        //-------------------------------//
        #region Constuctor

        /// <summary>
        /// Class constructor
        /// </summary>
        public ZeroitMatrixLabel()
    {
      //---------------------------------------------------
      // Initialisations 

      // Double bufferisation
      SetStyle(ControlStyles.DoubleBuffer |
               ControlStyles.UserPaint |
               ControlStyles.AllPaintingInWmPaint, true);

      // Size
      m_iNbLedLines   = 16;
      m_iNbLedRows    = 64;
      m_dLedSizeCoeff = 0.67;

      // Color
      m_bOnBrush  = new SolidBrush(Color.Red);
      m_bOffBrush = new SolidBrush(Color.DarkGray);

      // Style
      m_lsLedStyle = LedSyle.Round;

      // Led collection
      m_lItemList         = new List<LedMatrixItem>();
      m_llLedPropertyList = new List<List<LedProperty>>();

      // Timer
      m_tTimer = new System.Windows.Forms.Timer();
      m_tTimer.Interval = 70;
      m_tTimer.Tick +=new EventHandler(m_tTimer_Tick);
      m_uiTickCount = 0;

      // Id count
      m_iItemIdCount = -1;


      //---------------------------------------------------
      // Construction

      // Place the leds on the control
      SetMatrixSize(m_iNbLedLines, m_iNbLedRows);

     //------------------ZeroitSetText------------------------
     SetItemText(1, setText);

     }

        #endregion


        //-------------------------------//
        //         PROPERTIES            //
        //-------------------------------//

        #region Properties            
        

        /// <summary>
        /// Gets or sets the led style.
        /// </summary>
        /// <value>The led style.</value>
        public LedSyle LedStyle
        {
            get { return m_lsLedStyle; }
            set
            {
                m_lsLedStyle = value;
                Invalidate();
            }
        }
        /// <summary>
        /// Gets or sets the set text.
        /// </summary>
        /// <value>The set text.</value>
        public string SetText
        {
                get { return setText; }
                set
                {
                    setText = value;
                
                    Invalidate();
                }
        }

        /// <summary>
        /// Gets or sets the led rows.
        /// </summary>
        /// <value>The led rows.</value>
        [Category("Matrix size"),Description("Rows"),DefaultValue(64)]
        public int NbLedRows
        {
          get
          {
            return m_iNbLedRows;
          }
          set
          {
            m_iNbLedRows = value;
            SetMatrixSize(m_iNbLedLines, m_iNbLedRows);
            Invalidate();
          }
        }

        /// <summary>
        /// Gets or sets the led lines.
        /// </summary>
        /// <value>The led lines.</value>
        [Category("Matrix size"),Description("Lines"),DefaultValue(2)]
        public int NbLedLines
        {
          get
          {
            return m_iNbLedLines;
          }
          set
          {
            m_iNbLedLines = value;
            SetMatrixSize(m_iNbLedLines, m_iNbLedRows);
            Invalidate();
          }
        }

        /// <summary>
        /// Gets or sets the size coefficient.
        /// </summary>
        /// <value>The size coefficient.</value>
        [Category("Matrix size"),Description("Size coefficient"),DefaultValue(0.66)]
        public double SizeCoeff
        {
          get
          {
            return m_dLedSizeCoeff;
          }
          set
          {
            if ((value<0) || (value<1))
            {
              Invalidate();
            }
            m_dLedSizeCoeff = value;
            SetPositionAndSize();
            Invalidate();
          }
        }

        /// <summary>
        /// Gets or sets the color of the led when on.
        /// </summary>
        /// <value>The color of the led when on.</value>
        [Category("Led Color"),Description("On"),DefaultValue("192,45,0")]
        public Color LedOnColor
        {
          get
          {
            return m_bOnBrush.Color;
          }
          set
          {
            m_bOnBrush.Color = value;
            Invalidate();
            Refresh();
          }
        }

        /// <summary>
        /// Gets or sets the color of the led when off.
        /// </summary>
        /// <value>The color of the led when off.</value>
        [Category("Led Color"), Description("Off"), DefaultValue("192,111,105")]
        public Color LedOffColor
        {
          get
          {
            return m_bOffBrush.Color;
          }
          set
          {
            m_bOffBrush.Color = value;
            Invalidate();
            Refresh();
          }
        }

        #endregion


        //-------------------------------//
        //        MODIFICATORS           //
        //-------------------------------//
        #region Modificators

        /// <summary>
        /// Set the style of the led
        /// </summary>
        /// <param name="p_lsLedSyle">Led style</param>
        public void SetLedStyle(LedSyle p_lsLedSyle)
    {
      m_lsLedStyle = p_lsLedSyle;
      this.Refresh();
    }

        /// <summary>
        /// Define the number of leds of the control
        /// </summary>
        /// <param name="p_iNbLines">Number of led lines</param>
        /// <param name="p_iNbRows">Number of led rows</param>
        public void SetMatrixSize(int p_iNbLines,
                              int p_iNbRows)
    {
      // Reset the property list
      m_llLedPropertyList.Clear();

      //---------------------------------------------------
      // Add the led to the control

      // Loop on the lines
      for (int iIdxLine = 0; iIdxLine < p_iNbLines; iIdxLine++)
      {
        List<LedProperty> lLineLedProperty = new List<LedProperty>();

        // Loop on the rows
        for (int iIdxRow = 0; iIdxRow < p_iNbRows; iIdxRow++)
        {
          LedProperty lpNewProperty = new LedProperty();

          // Add the led in the line
          lLineLedProperty.Add(lpNewProperty);
        }

        // Add the line to the control
        m_llLedPropertyList.Add(lLineLedProperty);
      }

      //---------------------------------------------------
      // Define the led of the control

      SetPositionAndSize();

    }

        /// <summary>
        /// Place the led on the control and define their size
        /// </summary>
        private void SetPositionAndSize()
    {
      int iCellSize = 0;
      int iCellSizeFromWidth = 0;
      int iCellSizeFromHeight = 0;
      int iHeightMargin = 0;
      int iWidthMargin = 0;
      int iYLinepos = 0;
      int iXRowpos = 0;

      //---------------------------------------------------
      // Initialisations

      // Select the direction size leader
      iCellSizeFromWidth  = this.Width  / (m_llLedPropertyList[0].Count);
      iCellSizeFromHeight = this.Height / (m_llLedPropertyList.Count);
      if (iCellSizeFromHeight > iCellSizeFromWidth)
      {
        iCellSize = iCellSizeFromWidth;
      }
      else
      {
        iCellSize = iCellSizeFromHeight;
      }


      // Deduct the margin
      iWidthMargin = (this.Width - m_llLedPropertyList[0].Count * iCellSize) / 2;
      iHeightMargin = (this.Height - m_llLedPropertyList.Count * iCellSize) / 2;


      //---------------------------------------------------
      // Set position and size

      // Vertical adjustment  
      iYLinepos = iHeightMargin + iCellSize/2;

      // Loop on the lines
      for (int iIdxLine = 0; iIdxLine < m_llLedPropertyList.Count; iIdxLine++)
      {
        // Horizontal adjustment
        iXRowpos = iWidthMargin + iCellSize/2;

        // Loop on the rows
        for (int iIdxRow = 0; iIdxRow < m_llLedPropertyList[0].Count; iIdxRow++)
        {
          // Position
          m_llLedPropertyList[iIdxLine][iIdxRow].m_ptLocation.X = iXRowpos;
          m_llLedPropertyList[iIdxLine][iIdxRow].m_ptLocation.Y = iYLinepos;

          // Size
          m_llLedPropertyList[iIdxLine][iIdxRow].m_iRadius = (int)(iCellSize * m_dLedSizeCoeff/2);


          // Updates
          iXRowpos += iCellSize;
        }

        // Updates
        iYLinepos += iCellSize;
      }

      // Final refresh
      this.Refresh();
    }

        #endregion


        //-------------------------------//
        //           ACCESSOR            //
        //-------------------------------//
        #region Accessor

        /// <summary>
        /// Get the style of the led
        /// </summary>
        /// <returns>Led style</returns>
        public LedSyle GetLedStyle()
    {
      return m_lsLedStyle;
    }

        #endregion


        //-------------------------------//
        //        ITEM MANAGEMENT        //
        //-------------------------------//
        #region ItemMgt

        /// <summary>
        /// Get an item index in the item list by its Id
        /// </summary>
        /// <param name="p_iId">Id of the item</param>
        /// <returns>Index of the item</returns>
        private int GetItemIdxFromId(int p_iId)
    {
      int iIdx = 0;

      // Scan the item list
      foreach (LedMatrixItem lmiItem in m_lItemList)
      {
        // Id match ?
        if (lmiItem.GetId() == p_iId)
        {
          return iIdx;
        }
        iIdx++;
      }

      // Fail
      return -1;
    }


        /// <summary>
        /// Add an item to the item list
        /// </summary>
        /// <param name="p_bMasterLedOn">Led On definition of the new item</param>
        /// <param name="p_pLocation">Initial position of the new item</param>
        /// <param name="p_dDirection">Direction of the new item</param>
        /// <param name="p_sSpeed">Speed of the new item</param>
        /// <returns>Item Id</returns>
        public int AddItem(bool[,]       p_bMasterLedOn,
                       Point         p_pLocation,
                       ItemDirection p_dDirection,
                       ItemSpeed     p_sSpeed)
    {
      // Updates Id counting
      m_iItemIdCount++;

      // New item
      LedMatrixItem lmiMyNewItem = new LedMatrixItem(m_iItemIdCount,
                                                     p_bMasterLedOn,
                                                     p_pLocation,
                                                     p_dDirection,
                                                     p_sSpeed);

      // Add the item to the list
      m_lItemList.Add(lmiMyNewItem);

      // Return item Id
      return m_iItemIdCount;
    }

        /// <summary>
        /// Add an item built from text to the item list
        /// </summary>
        /// <param name="p_sText">New item text</param>
        /// <param name="p_pLocation">Initial position of the new item</param>
        /// <param name="p_dDirection">Direction of the new item</param>
        /// <param name="p_sSpeed">Speed of the new item</param>
        /// <returns>Item Id</returns>
        public int AddTextItem(string        p_sText,
                           Point         p_pLocation,
                           ItemDirection p_dDirection,
                           ItemSpeed     p_sSpeed)
    {
      // Updates Id counting
      m_iItemIdCount++;

      // New item
      LedMatrixItem lmiMyNewItem = new LedMatrixItem(m_iItemIdCount,
                                                     GetLedOnFromString(p_sText),
                                                     p_pLocation,
                                                     p_dDirection,
                                                     p_sSpeed);

      // Add the item to the list
      m_lItemList.Add(lmiMyNewItem);

      // Return item Id
      return m_iItemIdCount;
    }


        /// <summary>
        /// Set the current location of an item
        /// </summary>
        /// <param name="p_iItemId">Id of the item</param>
        /// <param name="p_pLocation">New current location</param>
        public void SetItemLocation(int p_iItemId,
                                Point p_pLocation)
    {
      int iItemIdx = -1;

      // Get the item index
      iItemIdx = GetItemIdxFromId(p_iItemId);
      if (iItemIdx == -1)
      {
        return;
      }

      // Set new location
      m_lItemList[iItemIdx].SetLocation(p_pLocation);
      this.Refresh();
    }


        /// <summary>
        /// Set the direction of an item
        /// </summary>
        /// <param name="p_iItemId">Id of the item</param>
        /// <param name="p_dDirection">New direction</param>
        public void SetItemDirection(int           p_iItemId,
                                 ItemDirection p_dDirection)
    {
      int iItemIdx = -1;

      // Get the item index
      iItemIdx = GetItemIdxFromId(p_iItemId);
      if (iItemIdx == -1)
      {
        return;
      }

      // Set new direction
      m_lItemList[iItemIdx].SetDirection(p_dDirection);
      return;
    }

        /// <summary>
        /// Set the speed of an item
        /// </summary>
        /// <param name="p_iItemId">Id of the item</param>
        /// <param name="p_sSpeed">New speed</param>
        public void SetItemSpeed(int       p_iItemId,
                             ItemSpeed p_sSpeed)
    {
      int iItemIdx = -1;

      // Get the item index
      iItemIdx = GetItemIdxFromId(p_iItemId);
      if (iItemIdx == -1)
      {
        return;
      }

      // Set new speed
      m_lItemList[iItemIdx].SetSpeed(p_sSpeed);
      return;
    }

        /// <summary>
        /// Set the led On definition of an item from text
        /// </summary>
        /// <param name="p_iItemId">Id of the item</param>
        /// <param name="p_sText">Text to put in the led On definition</param>
        public void SetItemText(int    p_iItemId,
                            string p_sText)
    {
      p_sText = setText;
      bool[,] bLedOn; 
      int     iItemIdx = -1;

      // Get the item index
      iItemIdx = GetItemIdxFromId(p_iItemId);
      if (iItemIdx == -1)
      {
        return;
      }

      // Convert text to led On
      bLedOn = GetLedOnFromString(p_sText);

      // Set the new led On definition
      m_lItemList[iItemIdx].SetLedOnArray(bLedOn);
      this.Refresh();
      return;
    }

        /// <summary>
        /// Move the items of the control
        /// </summary>
        /// <param name="p_uiTickCount">Clock ticks counting</param>
        private void MoveItems(uint p_uiTickCount)
    {
      // On each item
      foreach (LedMatrixItem lmiItem in m_lItemList)
      {
        // Move the item
        lmiItem.MoveItem(p_uiTickCount,m_llLedPropertyList.Count,m_llLedPropertyList[0].Count);
      }
      this.Refresh();
    }

        /// <summary>
        /// Start the movements of items
        /// </summary>
        /// <param name="i_IdlePeriod">Tick period</param>
        public void StartMove(int i_IdlePeriod)
    {
      m_tTimer.Interval = i_IdlePeriod;
      m_tTimer.Start();
    }

        /// <summary>
        /// Stop the movements of items
        /// </summary>
        public void StopMove()
    {
      m_tTimer.Stop();
    }

        #endregion


        //-------------------------------//
        //       SYBMOLS AND FONTS       //
        //-------------------------------//
        #region Symbols

        /// <summary>
        /// Get the led on array from a char defined in the font collection
        /// </summary>
        /// <param name="p_cSourceChar">Ascci code of the character</param>
        /// <returns>The corresponding led on array</returns>
        private bool[,] GetLedOnFromChar(char p_cSourceChar)
    {
      bool[,] bExclaim = new bool[4, 4];

      // Look in the font collection
      foreach (LedMatrixSymbol lmsSymbol in m_fcFontCollection.lstFontList[0].lstSymbolList)
      {
        // Match ?
        if (p_cSourceChar == lmsSymbol.uiCode)
        {
          return lmsSymbol.bLedOnMatrix;
        }
      }

      // Unfound symbol
      bExclaim[0, 0] = true;
      bExclaim[0, 2] = true;
      bExclaim[1, 0] = true;
      bExclaim[1, 2] = true;
      bExclaim[3, 0] = true;
      bExclaim[3, 2] = true;
      return bExclaim;
    }

        /// <summary>
        /// Get the led on array from a string
        /// </summary>
        /// <param name="p_sSourceString">String to convert</param>
        /// <returns>The corresponding led on array</returns>
        private bool[,] GetLedOnFromString(string p_sSourceString)
    {
      int           iMaxHeightChar = 0;
      int           iNbOfLedUsed   = 0;
      int           iLedCount      = 0;
      List<bool[,]> lstLedOnChar   = new List<bool[,]>();

      //----------------------------------------------------
      //Get the list of the ledOn array symbols form the string's char

      // Loop on the char
      for (int iIdxChar = 0; iIdxChar < p_sSourceString.Length; iIdxChar++)
      {
        // Get the corresponding array
        lstLedOnChar.Add(GetLedOnFromChar(p_sSourceString[iIdxChar]));

        // Memorize the maximum height size of the chars
        if (lstLedOnChar[lstLedOnChar.Count - 1].GetLength(0) > iMaxHeightChar)
        {
          iMaxHeightChar = lstLedOnChar[lstLedOnChar.Count - 1].GetLength(0);
        }

        // Updates the nb of led used by the all chars
        iNbOfLedUsed += lstLedOnChar[lstLedOnChar.Count - 1].GetLength(1);
      }

      //---------------------------------------------------
      // Create the return array

      // Allocation
      bool[,] bReturnLedOn = new bool[iMaxHeightChar, iNbOfLedUsed];

      // Loop on the char arrays
      foreach (bool[,] bCurrentLedOn in lstLedOnChar)
      {
        // Loop on lines
        for (int iIdxLine = 0; iIdxLine < bCurrentLedOn.GetLength(0); iIdxLine++)
        {
          // Loop on rows
          for (int iIdxRow = 0; iIdxRow < bCurrentLedOn.GetLength(1); iIdxRow++)
          {
            // Copy the ledOn
            bReturnLedOn[iIdxLine, iLedCount + iIdxRow] = bCurrentLedOn[iIdxLine, iIdxRow];
          }
        }

        // Update
        iLedCount += bCurrentLedOn.GetLength(1);
      }

      // Return
      return bReturnLedOn;
    }


        /// <summary>
        /// Load a font collection from the xml font file in the resources of the project
        /// </summary>
        /// <param name="sResourceName">Name of the s resource.</param>
        /// <returns>true for ok; false for fail</returns>
        public bool LoadFontCollectionFromResource(string sResourceName)
    {
      Assembly      aAssem      = Assembly.GetExecutingAssembly();
      Stream        srXmlStream = aAssem.GetManifestResourceStream(sResourceName);
      XmlTextReader rxReaderXml = new XmlTextReader(srXmlStream);

      return LoadFontCollectionFromXmlReader(rxReaderXml);
    }

        /// <summary>
        /// Load a font collection from an xml file
        /// </summary>
        /// <param name="p_sFontCollectionFileName">Font collection file</param>
        /// <returns>true for ok; false for fail</returns>
        public bool LoadFontCollectionFromFile(string p_sFontCollectionFileName)
    {
      XmlTextReader rxReaderXml = new XmlTextReader(p_sFontCollectionFileName);

      return LoadFontCollectionFromXmlReader(rxReaderXml);
    }

        /// <summary>
        /// Load the control font collection from an xml reader stream
        /// </summary>
        /// <param name="p_rxReaderXml">Xml reader stream</param>
        /// <returns>true for ok; false for fail</returns>
        private bool LoadFontCollectionFromXmlReader(XmlTextReader p_rxReaderXml)
    {
      LedMatrixSymbolFontCollection lmsfcReturnCollection;
      List<LedMatrixSymbolFont>     lstFontList = new List<LedMatrixSymbolFont>();

      //---------------------------------------------------
      // Initialisations

      // Init the reader
      p_rxReaderXml.WhitespaceHandling = WhitespaceHandling.None;

      //---------------------------------------------------
      // Read the file

      // While there is somthing to read
      while (p_rxReaderXml.Read())
      {
        LedMatrixSymbolFont lmsfNewFont;
        string sFontName;
        List<LedMatrixSymbol> lstSymbolList = new List<LedMatrixSymbol>();


        // End of font ?
        if (p_rxReaderXml.Name != "LedMatrixSymbolFont")
        {
          // Next one
          continue;
        }

        // New font
        sFontName = p_rxReaderXml.GetAttribute("FontName");

        // Identify the symbols of the font
        while (p_rxReaderXml.Read())
        {
          uint uiSmbCode = 0;
          string sDescription;
          bool[,] bLedOnMatrix;
          LedMatrixSymbol lmsNewSymbol;
          List<LedOnLine> lstLedOnLine = new List<LedOnLine>();

          // End of sybmols ?
          if (p_rxReaderXml.Name != "LedMatrixSymbol")
          {
            // Next one
            break;
          }

          // New symbol
          uiSmbCode = Convert.ToUInt32(p_rxReaderXml.GetAttribute("SymbolCode"));
          sDescription = p_rxReaderXml.GetAttribute("Description");

          // Identify the Led matrix of the symbol
          while (p_rxReaderXml.Read())
          {
            int iLineNumber = 0;
            string sLedOn;
            LedOnLine lolLedOnLine;

            // End of Led line
            if (p_rxReaderXml.Name != "LedLine")
            {
              // Next one
              break;
            }

            // New Led line
            iLineNumber = Convert.ToInt32(p_rxReaderXml.GetAttribute("LineNumber"));
            sLedOn = p_rxReaderXml.GetAttribute("LineLedOn");

            // Add the line to the list
            lolLedOnLine = new LedOnLine(iLineNumber, sLedOn);
            lstLedOnLine.Add(lolLedOnLine);

          }// Loop ont line processing

          // Convert the led lines to led matrix
          bLedOnMatrix = ConvertLedOnLineToLedOnMatrix(lstLedOnLine);

          // Create and add the symbol
          lmsNewSymbol = new LedMatrixSymbol(uiSmbCode, sDescription, bLedOnMatrix);
          lstSymbolList.Add(lmsNewSymbol);

        }// Loop on symbol processing

        // Create and add the font
        lmsfNewFont = new LedMatrixSymbolFont(sFontName, lstSymbolList);
        lstFontList.Add(lmsfNewFont);

      }// Loop on font processing

      // Create the return font collection
      lmsfcReturnCollection = new LedMatrixSymbolFontCollection(lstFontList);

      // Valid font collection ?
      if (lstFontList.Count == 0)
      {
        return false;
      }
      else if (lstFontList[0].lstSymbolList.Count == 0)
      {
        return false;
      }

      // Ok => copy the collection
      m_fcFontCollection = lmsfcReturnCollection;
      return true;
    }

        /// <summary>
        /// Convert a xml ledOn lines to a ledOn bool array
        /// </summary>
        /// <param name="p_lstLedOnLine">List of led led on line to convert</param>
        /// <returns>LedOn line array</returns>
        private bool[,] ConvertLedOnLineToLedOnMatrix(List<LedOnLine> p_lstLedOnLine)
    {
      int iMaxLineSize = 0;
      int iMaxLineNb = 0;
      bool[,] bReturnLedOnMatrix;

      // Get the size of the matrix
      foreach (LedOnLine lolLine in p_lstLedOnLine)
      {
        if (lolLine.sLedOn.Length > iMaxLineSize)
        {
          iMaxLineSize = lolLine.sLedOn.Length;
        }

        if (lolLine.iLineNo > iMaxLineNb)
        {
          iMaxLineNb = lolLine.iLineNo;
        }
      }

      // Creation of the return matrix
      bReturnLedOnMatrix = new bool[iMaxLineNb + 1, iMaxLineSize];

      // Build the matix
      foreach (LedOnLine lolLine in p_lstLedOnLine)
      {
        for (int iIdxChar = 0; iIdxChar < lolLine.sLedOn.Length; iIdxChar++)
        {
          if (lolLine.sLedOn[iIdxChar] == '#')
          {
            bReturnLedOnMatrix[lolLine.iLineNo, iIdxChar] = true;
          }
        }
      }

      // Return the led on matrix
      return bReturnLedOnMatrix;
    }

        #endregion


        //-------------------------------//
        //          EVENTS               //
        //-------------------------------//
        #region Events

        /// <summary>
        /// Timer tick event
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void m_tTimer_Tick(object sender, EventArgs e)
    {
      MoveItems(m_uiTickCount);
      m_uiTickCount++;
    }

        /// <summary>
        /// Rezize event
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      SetPositionAndSize();
    }




        #region Transparency


        #region Include in Paint

        private void TransInPaint(Graphics g)
        {
            if (AllowTransparency)
            {
                MakeTransparent(this, g);
            }
        }

        #endregion

        #region Include in Private Field

        private bool allowTransparency = true;

        #endregion

        #region Include in Public Properties

        public bool AllowTransparency
        {
            get { return allowTransparency; }
            set
            {
                allowTransparency = value;

                Invalidate();
            }
        }

        #endregion

        #region Method

        //-----------------------------Include in Paint--------------------------//
        //
        // if(AllowTransparency)
        //  {
        //    MakeTransparent(this,g);
        //  }
        //
        //-----------------------------Include in Paint--------------------------//

        private static void MakeTransparent(Control control, Graphics g)
        {
            var parent = control.Parent;
            if (parent == null) return;
            var bounds = control.Bounds;
            var siblings = parent.Controls;
            int index = siblings.IndexOf(control);
            Bitmap behind = null;
            for (int i = siblings.Count - 1; i > index; i--)
            {
                var c = siblings[i];
                if (!c.Bounds.IntersectsWith(bounds)) continue;
                if (behind == null)
                    behind = new Bitmap(control.Parent.ClientSize.Width, control.Parent.ClientSize.Height);
                c.DrawToBitmap(behind, c.Bounds);
            }
            if (behind == null) return;
            g.DrawImage(behind, control.ClientRectangle, bounds, GraphicsUnit.Pixel);
            behind.Dispose();
        }

        #endregion


        #endregion




        /// <summary>
        /// On paint event
        /// </summary>
        /// <param name="pe">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        protected override void OnPaint(PaintEventArgs pe)
        {
              TransInPaint(pe.Graphics);
              Graphics gfx = pe.Graphics;

              // Calling the base class OnPaint
              base.OnPaint(pe);

              // Antialiasing
              gfx.SmoothingMode = SmoothingMode.AntiAlias;

              // Return if non consistant led
              if (m_llLedPropertyList[0][0].m_iRadius == 0)
              {
                return;
              }

              // Loop on the led lines
              for (int iIdxLine = 0; iIdxLine < m_llLedPropertyList.Count; iIdxLine++)
              {
                // Loop on the led row
                for (int iIdxRow = 0; iIdxRow < m_llLedPropertyList[0].Count; iIdxRow++)
                {
                  bool bLedOn = false;

                  // Loop on the display items
                  foreach (LedMatrixItem diDisplayItem in m_lItemList)
                  {
                    // Corresponding point of the item
                    int iLinePtForItem = iIdxLine - diDisplayItem.GetLineOffset();
                    int iRowPtForItem = iIdxRow - diDisplayItem.GetRowOffset();

                    // Valid point ?
                    if ((iLinePtForItem < 0) || (iLinePtForItem >= diDisplayItem.GetMasterLedOn().GetLength(0)) ||
                       (iRowPtForItem < 0) || (iRowPtForItem >= diDisplayItem.GetMasterLedOn().GetLength(1)))
                    {
                      // Not valid => next item
                      continue;
                    }

                    // OR on the cell ledOn
                    bLedOn |= diDisplayItem.GetMasterLedOn()[iLinePtForItem, iRowPtForItem];
                  }

                  // State of the led
                  if (bLedOn == true)
                  {
                    if (m_lsLedStyle == LedSyle.Square)
                    {
                      pe.Graphics.FillRectangle(m_bOnBrush,
                      m_llLedPropertyList[iIdxLine][iIdxRow].m_ptLocation.X - m_llLedPropertyList[iIdxLine][iIdxRow].m_iRadius,
                      m_llLedPropertyList[iIdxLine][iIdxRow].m_ptLocation.Y - m_llLedPropertyList[iIdxLine][iIdxRow].m_iRadius,
                      m_llLedPropertyList[iIdxLine][iIdxRow].m_iRadius << 1,
                      m_llLedPropertyList[iIdxLine][iIdxRow].m_iRadius << 1);
                    }
                    else
                    {
                      pe.Graphics.FillPie(m_bOnBrush,
                        m_llLedPropertyList[iIdxLine][iIdxRow].m_ptLocation.X - m_llLedPropertyList[iIdxLine][iIdxRow].m_iRadius,
                        m_llLedPropertyList[iIdxLine][iIdxRow].m_ptLocation.Y - m_llLedPropertyList[iIdxLine][iIdxRow].m_iRadius,
                        m_llLedPropertyList[iIdxLine][iIdxRow].m_iRadius << 1,
                        m_llLedPropertyList[iIdxLine][iIdxRow].m_iRadius << 1,
                        0, 360);
                    }
                  }
                  else
                  {
                    if (m_lsLedStyle == LedSyle.Square)
                    {
                      pe.Graphics.FillRectangle(m_bOffBrush,
                      m_llLedPropertyList[iIdxLine][iIdxRow].m_ptLocation.X - m_llLedPropertyList[iIdxLine][iIdxRow].m_iRadius,
                      m_llLedPropertyList[iIdxLine][iIdxRow].m_ptLocation.Y - m_llLedPropertyList[iIdxLine][iIdxRow].m_iRadius,
                      m_llLedPropertyList[iIdxLine][iIdxRow].m_iRadius << 1,
                      m_llLedPropertyList[iIdxLine][iIdxRow].m_iRadius << 1);
                    }
                    else
                    {
                      pe.Graphics.FillPie(m_bOffBrush,
                        m_llLedPropertyList[iIdxLine][iIdxRow].m_ptLocation.X - m_llLedPropertyList[iIdxLine][iIdxRow].m_iRadius,
                        m_llLedPropertyList[iIdxLine][iIdxRow].m_ptLocation.Y - m_llLedPropertyList[iIdxLine][iIdxRow].m_iRadius,
                        m_llLedPropertyList[iIdxLine][iIdxRow].m_iRadius << 1,
                        m_llLedPropertyList[iIdxLine][iIdxRow].m_iRadius <<1,
                        0, 360);
                    }
                  }
                }
              }
    }

    #endregion
  }
}
