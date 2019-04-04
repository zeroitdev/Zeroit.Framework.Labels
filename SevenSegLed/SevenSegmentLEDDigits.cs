// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-28-2018
// ***********************************************************************
// <copyright file="SevenSegmentLEDDigits.cs" company="Zeroit Dev Technologies">
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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Zeroit.Framework.Labels.LedDisplays
{

    // *********************************** class ZeroitSegLedLabel

    /// <summary>
    /// A class collection for rendering seven led digits.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    public class ZeroitSegLedLabel : Control
        {

        /// <summary>
        /// The background color
        /// </summary>
        static Color    BACKGROUND_COLOR = SystemColors.Control;
        /// <summary>
        /// The border color
        /// </summary>
        static Color    BORDER_COLOR = Color.Black;
        /// <summary>
        /// The border thickness
        /// </summary>
        const int      BORDER_THICKNESS = 0;
        /// <summary>
        /// The control height
        /// </summary>
        const int      CONTROL_HEIGHT = 144;

        /// <summary>
        /// The height divisor
        /// </summary>
        const int      HEIGHT_DIVISOR = 13;
        /// <summary>
        /// The offset
        /// </summary>
        const int      OFFSET = CONTROL_HEIGHT / HEIGHT_DIVISOR;
        /// <summary>
        /// The width multiplier
        /// </summary>
        const int      WIDTH_MULTIPLIER = 7;

        /// <summary>
        /// The control width
        /// </summary>
        const int      CONTROL_WIDTH = WIDTH_MULTIPLIER * OFFSET;
        /// <summary>
        /// The decimal point height
        /// </summary>
        const int      DECIMAL_POINT_HEIGHT = OFFSET;
        /// <summary>
        /// The decimal point width
        /// </summary>
        const int      DECIMAL_POINT_WIDTH = OFFSET;
        /// <summary>
        /// The digit offset
        /// </summary>
        const int      DIGIT_OFFSET = WIDTH_MULTIPLIER * OFFSET;
        /// <summary>
        /// The digit value
        /// </summary>
        const int      DIGIT_VALUE = 8888;
        /// <summary>
        /// The format
        /// </summary>
        const string   FORMAT = "-2.2";
        /// <summary>
        /// The gap
        /// </summary>
        const int      GAP = 0;
        /// <summary>
        /// The maximum border thickness
        /// </summary>
        const int      MAXIMUM_BORDER_THICKNESS = 5;
        /// <summary>
        /// The maximum decimals
        /// </summary>
        const int      MAXIMUM_DECIMALS = 5;
        /// <summary>
        /// The maximum digits
        /// </summary>
        const int      MAXIMUM_DIGITS = 4;
        /// <summary>
        /// The maximum gap
        /// </summary>
        const int      MAXIMUM_GAP = 5;
        /// <summary>
        /// The maximum slant
        /// </summary>
        const float    MAXIMUM_SLANT = 0.0F;
        /// <summary>
        /// The minimum border thickness
        /// </summary>
        const int      MINIMUM_BORDER_THICKNESS = 0;
        /// <summary>
        /// The minimum gap
        /// </summary>
        const int      MINIMUM_GAP = 0;
        /// <summary>
        /// The minimum slant
        /// </summary>
        const float    MINIMUM_SLANT = -0.4F;
        /// <summary>
        /// The minus sign height
        /// </summary>
        const int      MINUS_SIGN_HEIGHT = OFFSET;
        /// <summary>
        /// The minus sign width
        /// </summary>
        const int      MINUS_SIGN_WIDTH = 3 * OFFSET;
        /// <summary>
        /// The number digits
        /// </summary>
        const int      NUMBER_DIGITS = 1;
        /// <summary>
        /// The segment color
        /// </summary>
        static Color    SEGMENT_COLOR = Color.Red;
        /// <summary>
        /// The segments per digit
        /// </summary>
        const int      SEGMENTS_PER_DIGIT = 7;
        /// <summary>
        /// The slant
        /// </summary>
        const float    SLANT = -0.1F;

        // ***********************************************************

        /// <summary>
        /// The background
        /// </summary>
        GraphicsBuffer background = null;
        /// <summary>
        /// The background color
        /// </summary>
        Color background_color = BACKGROUND_COLOR;
        /// <summary>
        /// The border color
        /// </summary>
        Color border_color = BORDER_COLOR;
        /// <summary>
        /// The border thickness
        /// </summary>
        int border_thickness = BORDER_THICKNESS;
        /// <summary>
        /// The control height
        /// </summary>
        int control_height = CONTROL_HEIGHT;
        /// <summary>
        /// The control width
        /// </summary>
        int control_width = CONTROL_WIDTH;
        /// <summary>
        /// The decimal point height
        /// </summary>
        int decimal_point_height = OFFSET;
        /// <summary>
        /// The decimal point width
        /// </summary>
        int decimal_point_width = OFFSET;
        /// <summary>
        /// The digit value
        /// </summary>
        int digit_value = DIGIT_VALUE;
        /// <summary>
        /// The format
        /// </summary>
        string format = FORMAT;
        /// <summary>
        /// The format index
        /// </summary>
        int format_index = -1;
        /// <summary>
        /// The gap
        /// </summary>
        int gap = MINIMUM_GAP;
        /// <summary>
        /// The have decimal point
        /// </summary>
        bool have_decimal_point = false;
        /// <summary>
        /// The have minus sign
        /// </summary>
        bool have_minus_sign = false;
        /// <summary>
        /// The indicator
        /// </summary>
        GraphicsBuffer indicator = null;
        /// <summary>
        /// The minus sign height
        /// </summary>
        int minus_sign_height = MINUS_SIGN_HEIGHT;
        /// <summary>
        /// The minus sign width
        /// </summary>
        int minus_sign_width = MINUS_SIGN_WIDTH;
        /// <summary>
        /// The number digits
        /// </summary>
        int number_digits = NUMBER_DIGITS;
        /// <summary>
        /// The offset
        /// </summary>
        int offset = OFFSET;
        /// <summary>
        /// The offset div 2
        /// </summary>
        int offset_div_2 = OFFSET / 2;
        /// <summary>
        /// The revise background graphic
        /// </summary>
        bool revise_background_graphic = true;
        /// <summary>
        /// The segment color
        /// </summary>
        Color segment_color = SEGMENT_COLOR;
        /// <summary>
        /// The segment horizontal
        /// </summary>
        bool[ ]        segment_horizontal = new bool [
                                             SEGMENTS_PER_DIGIT ] {
                                                true,            // 0
                                                false,           // 1
                                                false,           // 2
                                                true,            // 3
                                                false,           // 4
                                                false,           // 5
                                                true };          // 6
                                                                 // declare the LED segments 
                                                                 // that are lit for each digit
                                                                 // LED segments are numbered 
                                                                 // from top to bottom, and 
                                                                 // from left to right
                                                                 /// <summary>
                                                                 /// The segments lit
                                                                 /// </summary>
        int[ ] [ ]     segments_lit = new int [ ] [ ] {
                            new int [ ] { 0, 1, 2, 4, 5, 6 },    // 0
                            new int [ ] { 2, 5 },                // 1
                            new int [ ] { 0, 2, 3, 4, 6 },       // 2
                            new int [ ] { 0, 2, 3, 5, 6 },       // 3
                            new int [ ] { 1, 2, 3, 5 },          // 4
                            new int [ ] { 0, 1, 3, 5, 6 },       // 5
                            new int [ ] { 1, 3, 4, 5, 6 },       // 6
                            new int [ ] { 0, 2, 5 },             // 7
                            new int [ ] { 0, 1, 2, 3, 4, 5, 6 }, // 8
                            new int [ ] { 0, 1, 2, 3, 5 } };     // 9
        /// <summary>
        /// The slant
        /// </summary>
        float slant = SLANT;
        /// <summary>
        /// The transparent
        /// </summary>
        bool transparent = true;
        /// <summary>
        /// The ul decimal point
        /// </summary>
        Point[ ]       UL_decimal_point = new Point [ 
                                                MAXIMUM_DECIMALS ];
        /// <summary>
        /// The ul digit
        /// </summary>
        Point[ ]       UL_digit = new Point [ MAXIMUM_DIGITS ];
        /// <summary>
        /// The ul minus sign
        /// </summary>
        Point UL_minus_sign = new Point ( 0, 0 );
        /// <summary>
        /// The ul segment offset
        /// </summary>
        Point[ ]       UL_segment_offset = new Point [ 
                                                SEGMENTS_PER_DIGIT ];

        // ******************************************** memory_cleanup

        /// <summary>
        /// Memories the cleanup.
        /// </summary>
        void memory_cleanup ( )
            {
                                        // DeleteGraphicsBuffer 
                                        // returns null
            if ( background != null )
                {
                background = background.DeleteGraphicsBuffer ( );
                }

            if ( indicator != null )
                {
                indicator = indicator.DeleteGraphicsBuffer ( );
                }
            }

        // ******************************* initialize_point_structures

        /// <summary>
        /// Initializes the point structures.
        /// </summary>
        void initialize_point_structures ( )
            {

            for ( int i = 0; ( i < MAXIMUM_DECIMALS ); i++ )
                {
                UL_decimal_point [ i ] = new Point ( 0, 0 );
                }
            
            for ( int i = 0; ( i < MAXIMUM_DIGITS ); i++ )
                {
                UL_digit [ i ] = new Point ( 0, 0 );
                }
            
            for ( int i = 0; ( i < SEGMENTS_PER_DIGIT ); i++ )
                {
                UL_segment_offset [ i ] = new Point ( 0, 0 );
                }
            }

        // ************************************* ZeroitSegLedLabel

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitSegLedLabel" /> class.
        /// </summary>
        public ZeroitSegLedLabel ( )
            {
            
            initialize_point_structures ( );
            parse_format ( format );
            adjust_control_dimensions_from_height ( CONTROL_HEIGHT );

            this.SetStyle ( 
                ( ControlStyles.DoubleBuffer |
                  ControlStyles.UserPaint |
                  ControlStyles.AllPaintingInWmPaint | 
                  ControlStyles.SupportsTransparentBackColor ),
                true );
            this.UpdateStyles();

            
            }

        // ***************************************************** round

        // http://en.wikipedia.org/wiki/Rounding

        /// <summary>
        /// Rounds the specified control value.
        /// </summary>
        /// <param name="control_value">The control value.</param>
        /// <returns>System.Int32.</returns>
        int round ( double control_value )
            {
            
            return ( ( int ) ( control_value + 0.5 ) );
            }

        // ********************************* create_background_graphic

        /// <summary>
        /// Creates the background graphic.
        /// </summary>
        void create_background_graphic ( )
            {

            if ( background != null )
                {
                background = background.DeleteGraphicsBuffer ( );
                }
            background = new GraphicsBuffer ( );
            background.CreateGraphicsBuffer ( control_width,
                                              control_height );
            background.Graphic.SmoothingMode = SmoothingMode.
                                               HighQuality;
            }

        // ********************************** create_indicator_graphic

        /// <summary>
        /// Creates the indicator graphic.
        /// </summary>
        void create_indicator_graphic ( )
            {

            if ( indicator != null )
                {
                indicator = indicator.DeleteGraphicsBuffer ( );
                }
            indicator = new GraphicsBuffer ( );
            indicator.CreateGraphicsBuffer ( control_width,
                                             control_height );
            indicator.Graphic.SmoothingMode = SmoothingMode.
                                              HighQuality;
            }

        // ************************************ zero_drawing_variables

        /// <summary>
        /// Zeroes the drawing variables.
        /// </summary>
        void zero_drawing_variables ( )
            {
            
            control_height = 0;
            control_width = 0;
            decimal_point_height = 0;
            decimal_point_width = 0;
            minus_sign_height = 0;
            minus_sign_width = 0;
            offset = 0;
            UL_minus_sign.X = 0;
            UL_minus_sign.Y = 0;
            for ( int i = 0; ( i < MAXIMUM_DECIMALS ); i++ )
                {
                UL_decimal_point [ i ].X = 0;
                UL_decimal_point [ i ].Y = 0;
                }
            for ( int i = 0; ( i < MAXIMUM_DIGITS ); i++ )
                {
                UL_digit [ i ].X = 0;
                UL_digit [ i ].Y = 0;
                }
            for ( int i = 0; ( i < UL_segment_offset.Length ); i++ )
                {
                UL_segment_offset [ i ].X = 0;
                UL_segment_offset [ i ].Y = 0;
                }
            }

        // ********************* adjust_control_dimensions_from_height

        /// <summary>
        /// Adjusts the height of the control dimensions from.
        /// </summary>
        /// <param name="new_height">The new height.</param>
        void adjust_control_dimensions_from_height ( int new_height )
            {
            int     decimal_point_x = 0;
            int     decimal_point_y = 0;
            int     digit_x = 0;
            int     digit_headspace = 0;
            int     i;
            int     left_shift = 0;
            double  shift = 0.0;

            zero_drawing_variables ( );

                                    // ch = 11 * o + 4 * g + 2 * o
                                    //    = 13 * o + 4 * g          =>
                                    // 13 * o = nh - 4 * g
                                    // o = ( nh - 4 * g ) / 13

            offset = round ( ( double ) ( new_height - ( 4 * gap ) ) /
                             ( double ) HEIGHT_DIVISOR );

            if ( ( offset & 0x01 ) != 0 )   // odd?
                {
                offset++;
                }
            offset_div_2 = offset / 2;

            control_height = ( HEIGHT_DIVISOR * offset ) +
                             ( 4 * gap );

            if ( ( control_height & 0x01 ) != 0 )
                {
                control_height--;
                }

            shift = ( double ) Slant;
            if ( shift < 0 )
                {
                shift = -shift;
                }
            left_shift = round ( shift * ( double ) control_height );

            decimal_point_height = offset;
            decimal_point_width = offset;

            minus_sign_height = offset;
            minus_sign_width = 3 * offset;

            UL_minus_sign.X = offset_div_2 + left_shift;
            UL_minus_sign.Y = 
                round ( ( double ) ( control_height - 
                                     minus_sign_height ) / 2.0 );

            decimal_point_x = offset_div_2 + left_shift;
            if ( have_minus_sign )
                {
                decimal_point_x += minus_sign_width;
                }
            decimal_point_y = control_height - 
                              ( 2 * offset )- 
                              offset_div_2;

            digit_x = decimal_point_x + decimal_point_width;
            digit_headspace = WIDTH_MULTIPLIER * offset + gap;

            for ( i = 0; ( i < MAXIMUM_DECIMALS ); i++ )
                {
                UL_decimal_point [ i ].X = decimal_point_x;
                UL_decimal_point [ i ].Y = decimal_point_y;
                
                decimal_point_x += digit_headspace;
                }

            for ( i = 0; ( i < MAXIMUM_DIGITS ); i++ )
                {
                UL_digit [ i ].X = digit_x;
                UL_digit [ i ].Y = offset_div_2;
                
                digit_x += digit_headspace;
                }

            control_width = offset_div_2 + left_shift;
            if ( have_minus_sign )
                {
                control_width += minus_sign_width;
                }
            control_width += decimal_point_width;
            control_width += number_digits * digit_headspace;
            control_width += offset_div_2;
            control_width += MAXIMUM_DIGITS * gap;

            i = 0;
                                                                // 0
            UL_segment_offset [ i ].X = offset_div_2 + gap;
            UL_segment_offset [ i++ ].Y = 0;
                                                                // 1
            UL_segment_offset [ i ].X = 0;
            UL_segment_offset [ i++ ].Y = offset_div_2 + gap;
                                                                // 2
            UL_segment_offset [ i ].X = ( 5 * offset ) + ( 2 * gap );
            UL_segment_offset [ i++ ].Y = offset_div_2 + gap;
                                                                // 3
            UL_segment_offset [ i ].X = offset_div_2 + gap;
            UL_segment_offset [ i++ ].Y =  ( 5 * offset ) + 
                                           ( 2 * gap );
                                                                // 4
            UL_segment_offset [ i ].X = 0;
            UL_segment_offset [ i++ ].Y = ( 5 * offset ) + 
                                          offset_div_2 + 
                                          ( 3 * gap );
                                                                // 5
            UL_segment_offset [ i ].X = ( 5 * offset ) + ( 2 * gap );
            UL_segment_offset [ i++ ].Y = ( 5 * offset ) + 
                                          offset_div_2 + 
                                          ( 3 * gap );
                                                                // 6
            UL_segment_offset [ i ].X = offset_div_2 + gap;
            UL_segment_offset [ i++ ].Y = ( 10 * offset ) + 
                                          ( 4 * gap);;

            this.Height = control_height;
            this.Width = control_width;
            }

        // *********************************** draw_background_graphic

        /// <summary>
        /// Draws the background graphic.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        void draw_background_graphic ( Graphics graphics )
            {
            
            if ( ! transparent )
                {
                graphics.Clear ( background_color );
                }
            }

        // *********************************** draw_horizontal_segment

        /// <summary>
        /// Draws the horizontal segment.
        /// </summary>
        /// <param name="UL_corner">The ul corner.</param>
        /// <param name="graphics">The graphics.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="pen">The pen.</param>
        void draw_horizontal_segment ( Point    UL_corner,
                                       Graphics graphics,
                                       Brush    brush,
                                       Pen      pen )
            {
            int         i = 0;
            Point [ ]   points = new Point [ SEGMENTS_PER_DIGIT ];

            i = 0;
            points [ i++ ] = new Point (                        // H1
                UL_corner.X, 
                UL_corner.Y + offset_div_2 );
            points [ i++ ] = new Point (                        // H2
                UL_corner.X + offset_div_2, 
                UL_corner.Y );
            points [ i++ ] = new Point (                        // H3
                UL_corner.X + 4 * offset + offset_div_2, 
                UL_corner.Y );
            points [ i++ ] = new Point (                        // H4
                UL_corner.X + 5 * offset, 
                UL_corner.Y + offset_div_2 );
            points [ i++ ] = new Point (                        // H5
                UL_corner.X + 4 * offset + offset_div_2, 
                UL_corner.Y + offset );
            points [ i++ ] = new Point (                        // H6
                UL_corner.X + offset_div_2, 
                UL_corner.Y + offset );
                                        // close polygon
            points [ i++ ] = new Point (                        // H1
                UL_corner.X, 
                UL_corner.Y + offset_div_2 );

            graphics.FillPolygon ( brush, points );

            if ( border_thickness > 0 )
                {
                graphics.DrawPolygon ( pen, points );
                }
            }

        // ************************************* draw_vertical_segment

        /// <summary>
        /// Draws the vertical segment.
        /// </summary>
        /// <param name="UL_corner">The ul corner.</param>
        /// <param name="graphics">The graphics.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="pen">The pen.</param>
        void draw_vertical_segment ( Point    UL_corner,
                                     Graphics graphics,
                                     Brush    brush,
                                     Pen      pen )
            {
            int         i = 0;
            Point [ ]   points = new Point [ SEGMENTS_PER_DIGIT ];

            i = 0;
            points [ i++ ] = new Point (                        // V1
                UL_corner.X, 
                UL_corner.Y + offset_div_2 );
            points [ i++ ] = new Point (                        // V2
                UL_corner.X + offset_div_2, 
                UL_corner.Y );
            points [ i++ ] = new Point (                        // V3
                UL_corner.X + offset, 
                UL_corner.Y + offset_div_2 );
            points [ i++ ] = new Point (                        // V4
                UL_corner.X + offset, 
                UL_corner.Y + 4 * offset + offset_div_2 );
            points [ i++ ] = new Point (                        // V5
                UL_corner.X + offset_div_2, 
                UL_corner.Y + 5 * offset );
            points [ i++ ] = new Point (                        // V6
                UL_corner.X, 
                UL_corner.Y  + 4 * offset + offset_div_2 );
                                        // close polygon
            points [ i++ ] = new Point (                        // V1
                UL_corner.X, 
                UL_corner.Y + offset_div_2 );

            graphics.FillPolygon ( brush, points );

            if ( border_thickness > 0 )
                {
                graphics.DrawPolygon ( pen, points );
                }
            }

        // ************************************************ add_points

        /// <summary>
        /// Adds the points.
        /// </summary>
        /// <param name="P1">The p1.</param>
        /// <param name="P2">The p2.</param>
        /// <returns>Point.</returns>
        Point add_points ( Point  P1,
                           Point  P2 )
            {

            return ( new Point ( P1.X + P2.X,
                                 P1.Y + P2.Y ) );
            }

        // ************************************ draw_indicator_graphic

        /// <summary>
        /// Draws the indicator graphic.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        void draw_indicator_graphic ( Graphics graphics )
            {
            int     abs_digit_value = 0;
            int     digit = 0;
            Matrix  matrix = new Matrix ( );
            Brush   segment_brush = new SolidBrush ( segment_color );
            Pen     segment_pen = new Pen ( 
                                      border_color, 
                                      ( float ) border_thickness );
                                        // apply the shear to the 
                                        // minum sign and digits
            matrix.Shear ( Slant, 0.0F );
            graphics.Transform = matrix;
                                        // draw any minus sign
            if ( have_minus_sign && ( digit_value < 0 ) )
                {
                Rectangle rectangle = 
                    new Rectangle ( UL_minus_sign,
                                    new Size ( minus_sign_width,
                                               minus_sign_height ) );

                graphics.FillRectangle ( segment_brush, rectangle );

                if ( border_thickness > 0 )
                    {
                    graphics.DrawRectangle ( segment_pen, rectangle);
                    }
                }
                                        // already have the minus sign
                                        // so no need for negative 
                                        // value
            abs_digit_value = digit_value;
            if ( abs_digit_value < 0 )
                {
                abs_digit_value = -abs_digit_value;
                }
                                        // draw the digits from last
                                        // to first
            for ( int i = ( number_digits - 1 ); ( i >= 0 ); i-- )
                {
                int [ ]  lit;

                digit = abs_digit_value % 10;
                abs_digit_value /= 10;

                lit = segments_lit [ digit ];
                foreach ( int segment in lit )
                    {
                    Point  UL_corner;

                    UL_corner = add_points ( 
                                    UL_digit [ i ],
                                    UL_segment_offset [ segment ] );
                    if ( segment_horizontal [ segment ] )
                        {
                        draw_horizontal_segment ( UL_corner,
                                                  graphics,
                                                  segment_brush,
                                                  segment_pen );
                        }
                    else
                        {
                        draw_vertical_segment ( UL_corner,
                                                graphics,
                                                segment_brush,
                                                segment_pen );
                        }
                    }
                }
                                        // clear the shear
            matrix.Shear ( 0.0F, 0.0F );
            graphics.Transform = matrix;
                                        // draw any decimal point
            if ( have_decimal_point )
                {
                if ( format_index >=0 )
                    {
                    Rectangle rectangle = new Rectangle (
                                UL_decimal_point [ format_index ],
                                new Size ( decimal_point_width,
                                           decimal_point_height ) );

                    graphics.FillEllipse ( segment_brush, rectangle );

                    if ( border_thickness > 0 )
                        {
                        graphics.DrawEllipse ( segment_pen, 
                                               rectangle);
                        }
                    }
                }

            segment_brush.Dispose ( );
            segment_pen.Dispose ( );
            }

        // ****************************************** Background_Color

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        [Category ( "Appearance" ),
         Description ( "Gets/Sets control background color" ),
         DefaultValue ( typeof ( SystemColors ), "Control" ),
         Bindable ( true )]
        public Color BackgroundColor
            {
            
            get
                {
                return ( background_color );
                }
            set
                {
                Color  old_value = background_color;
 
                background_color = value;
                if ( old_value != background_color )
                    {
                    revise_background_graphic = true;
                    this.Invalidate ( );
                    }
                }
            }

        // ********************************************** Border_Color

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        [Category ( "Appearance" ),
         Description ( "Gets/Sets control border color" ),
         DefaultValue ( typeof ( Color ), "Black" ),
         Bindable ( true )]
        public Color BorderColor
            {
            
            get
                {
                return ( border_color );
                }
            set
                {
                Color  old_value = border_color;
 
                border_color = value;
                if ( old_value != border_color )
                    {
                    revise_background_graphic = true;
                    this.Invalidate ( );
                    }
                }
            }

        // ****************************************** Border_Thickness

        /// <summary>
        /// Gets or sets the border thickness.
        /// </summary>
        /// <value>The border thickness.</value>
        [Category ( "Appearance" ),
         Description ( "Gets/Sets segment border_thickness" ),
         DefaultValue ( 0 ),
         Bindable ( true )]
        public int BorderThickness
            {
            
            get
                {
                return ( border_thickness );
                }
            set
                {
                int  old_value = border_thickness;
 
                border_thickness = value;
                if ( old_value != border_thickness )
                    {
                    if ( border_thickness > MAXIMUM_BORDER_THICKNESS )
                        {
                        border_thickness = MAXIMUM_BORDER_THICKNESS ;
                        }
                    if ( border_thickness < MINIMUM_BORDER_THICKNESS )
                        {
                        border_thickness = MINIMUM_BORDER_THICKNESS ;
                        }
                        
                    revise_background_graphic = true;
                    this.Invalidate ( );
                    }
                }
            }

        // ******************************************** Control_Height

        /// <summary>
        /// Gets or sets the height of the control.
        /// </summary>
        /// <value>The height of the control.</value>
        [Category ( "Appearance" ),
         Description ( "Gets/Sets control height" ),
         DefaultValue ( 100 ),
         Bindable ( true )]
        public int ControlHeight
            {
            
            get
                {
                return ( control_height );
                }
            set
                {
                int  old_value = control_height;
 
                control_height = value;
                if ( old_value != control_height )
                    {
                    adjust_control_dimensions_from_height ( value );
                    revise_background_graphic = true;
                    this.Invalidate ( );
                    }
                }
            }

        // ********************************************** parse_format

        // [-]d[.[d]]

        /// <summary>
        /// Parses the format.
        /// </summary>
        /// <param name="new_format">The new format.</param>
        /// <exception cref="ArgumentException">Format of the format string is " +
        ///                             "invalid</exception>
        void parse_format ( string  new_format )
            {
            const int   MAXIMUM_PIECES = 4;
            
            bool        done = false;
            bool        error = false;
            int         first_digit = 0;
            bool        found = false;
            bool        have_first_digit = false;
            int         i = 0;
            int         length = 0;
            int         second_digit = 0;
            string [ ]  pieces = new string [ MAXIMUM_PIECES ];
            
            have_decimal_point = false;
            have_minus_sign = false;
            
            for ( i = 0; ( i < new_format.Length ); i++ )
                {
                pieces [ i ] = new_format.Substring ( i, 1 );
                }
                
            length = pieces.Length - 1;
            done = ( length == 0 );
            while ( ! ( done || found ) )
                {
                found = ( pieces [ length ] != null );
                if ( ! found )
                    {
                    length--;
                    done = ( length == 0 );
                    }
                }
            length++;
            
            i = 0;
            done = ( i >= length );
            while ( ! ( done || error ) )
                {
                string  piece = pieces [ i ];
                
                if ( piece.Equals ( "-" ) )
                    {
                    if ( i == 0 )
                        {
                        have_minus_sign = true;
                        }
                    else
                        {
                        error = true;
                        }
                    }
                else if ( piece.Equals ( "." ) )
                    {
                    have_decimal_point = true;
                    }
                else if ( piece.Length > 1 )
                    {
                    error = true;
                    }
                else if ( Char.IsDigit ( piece, 0 ) )
                    {
                    if ( have_first_digit )
                        {
                        second_digit = Convert.ToInt32 ( piece );
                        }
                    else
                        {
                        have_first_digit = true;
                        first_digit = Convert.ToInt32 ( piece );
                        }
                    }
                else
                    {
                    error = true;
                    }

                if ( ! error )
                    {
                    i++;
                    done = ( i >= length );
                    }
                }
            
            if ( done )
                {
                format_index = first_digit;
                
                number_digits = first_digit + second_digit;
                if ( ( number_digits <= 0 ) ||
                     ( number_digits > MAXIMUM_DIGITS ) )
                    {
                    error = true;
                    }
                }
                
            if ( error )
                {
                throw ( new ArgumentException ( 
                            "Format of the format string is " +
                            "invalid" ) );
                }
            }

        // **************************************************** Format

        /// <summary>
        /// Gets or sets the format of how digits are displayed.
        /// </summary>
        /// <value>The format.</value>
        [Category ( "Appearance" ),
         Description ( "Gets/Sets format how digits are displayed" ),
         DefaultValue ( typeof ( string ), "2" ),
         Bindable ( true )]
        public string Format
            {
        
            get
                {
                return ( format );
                }
            set
                {
                string  old_value = format;
 
                format = value;
                if ( old_value != format )
                    {
                    parse_format ( format );
                    adjust_control_dimensions_from_height ( 
                                                        this.Height );
                    revise_background_graphic = true;
                    this.Invalidate ( );
                    }
                }
            }

        // ******************************************************* Gap

        /// <summary>
        /// Gets or sets the intersegment gap.
        /// </summary>
        /// <value>The gap.</value>
        [Category ( "Appearance" ),
         Description ( "Gets/Sets intersegment gap" ),
         DefaultValue ( 0 ),
         Bindable ( true )]
        public int Gap
            {

            get
                {
                return ( gap );
                }
            set
                {
                if ( value != gap )
                    {
                    gap = value;
                    if ( gap < MINIMUM_GAP )
                        {
                        gap = MINIMUM_GAP;
                        }
                    if ( gap > MAXIMUM_GAP )
                        {
                        gap = MAXIMUM_GAP;
                        }

                    adjust_control_dimensions_from_height ( 
                                                control_height );
                    revise_background_graphic = true;
                    this.Invalidate ( );
                    }
                }
            }

        // ********************************************* Segment_Color

        /// <summary>
        /// Gets or sets the color of the segment.
        /// </summary>
        /// <value>The color of the segment.</value>
        [Category ( "Appearance" ),
         Description ( "Gets/Sets digit lit segment color" ),
         DefaultValue ( typeof ( Color ), "Red" ),
         Bindable ( true )]
        public Color SegmentColor
            {
            
            get
                {
                return ( segment_color );
                }
            set
                {
                Color   old_value = segment_color;
 
                segment_color = value;
                if ( old_value != segment_color )
                    {
                    this.Invalidate ( );
                    }
                }
            }

        // ***************************************************** Slant

        /// <summary>
        /// Gets or sets the amount of digit display slant.
        /// </summary>
        /// <value>The slant.</value>
        [Category ( "Appearance" ),
         Description ( "Gets/Sets the amount of digit display slant" ),
         DefaultValue ( 0.0F ),
         Bindable ( true )]
        public float Slant
            {
            
            get
                {
                return ( slant );
                }
            set
                {
                float   old_value = slant;
 
                if ( old_value != value )
                    {
                    slant = value;
                    if ( slant > MAXIMUM_SLANT )
                        {
                        slant = MAXIMUM_SLANT;
                        }
                    if ( slant < MINIMUM_SLANT )
                        {
                        slant = MINIMUM_SLANT;
                        }

                    adjust_control_dimensions_from_height ( 
                                                    this.Height );
                    revise_background_graphic = true;
                    this.Invalidate ( );
                    }
                }
            }

        // *********************************************** Transparent

        /// <summary>
        /// Gets or sets a value indicating whether the control background is transparent.
        /// </summary>
        /// <value><c>true</c> if transparent; otherwise, <c>false</c>.</value>
        [Category ( "Appearance" ),
         Description ( "Gets/Sets whether control background is transparent" ),
         DefaultValue ( typeof ( bool ), "true" ),
         Bindable ( true )]
        public bool Transparent
            {
            
            get
                {
                return ( transparent );
                }
             set
                {
                bool  old_value = transparent;
 
                transparent = value;
                if ( old_value != transparent )
                    {
                    revise_background_graphic = true;
                    this.Invalidate ( );
                    }
                }
            }

        // ***************************************************** Value

        /// <summary>
        /// Gets or sets the value displayed.
        /// </summary>
        /// <value>The value.</value>
        [Category ( "Appearance" ),
         Description ( "Gets/Sets the value displayed" ),
         DefaultValue ( 8 ),
         Bindable ( true )]
        public int Value
            {
            
            get
                {
                return ( digit_value );
                }
            set 
                {
                int  old_value = digit_value;
 
                digit_value = value;
                if ( old_value != digit_value )
                    {
                    this.Invalidate ( );
                    }
                }
            }

        // ****************************************** OnControlRemoved

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.ControlRemoved" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs" /> that contains the event data.</param>
        protected override void OnControlRemoved ( 
                                                ControlEventArgs e )
            {
            
            base.OnControlRemoved ( e );
            
            memory_cleanup ( );
            }

        // *************************************************** OnPaint

        // see community additions in
        // http://msdn.microsoft.com/en-us/library/wk5b13s4(v=vs.90).aspx
        // regarding transparent background_graphic




        

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint ( PaintEventArgs e )
        {
            
            if ( transparent )
                {
                Rectangle           clip;
                PaintEventArgs      pea;
                GraphicsContainer   state;
                                            
                state = e.Graphics.BeginContainer();
                e.Graphics.TranslateTransform ( -this.Left, 
                                                -this.Top );
                clip = e.ClipRectangle;
                clip.Offset ( this.Left, this.Top );
                pea = new PaintEventArgs ( e.Graphics, clip );
                                        // paint the container's 
                                        // background
                InvokePaintBackground ( this.Parent, pea );
                                        // paint the container's 
                                        // foreground
                InvokePaint ( this.Parent, pea );
                                        // restores graphics to 
                                        // original state
                e.Graphics.EndContainer(state);
                }                       // end transparent background

            if ( ( background == null ) || revise_background_graphic )
                {
                if ( revise_background_graphic )
                    {
                    revise_background_graphic = false;
                    }
                create_background_graphic ( );
                draw_background_graphic ( background.Graphic );
                }
            background.RenderGraphicsBuffer ( e.Graphics );

            create_indicator_graphic ( );
            draw_indicator_graphic ( indicator.Graphic );

            indicator.RenderGraphicsBuffer ( e.Graphics );
            }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            ControlHeight = (Height + Width) / 4;

            Invalidate();

        }

    } // class ZeroitSegLedLabel

    // ****************************************** class GraphicsBuffer

    /// <summary>
    /// Class GraphicsBuffer.
    /// </summary>
    public class GraphicsBuffer
        {
        /// <summary>
        /// The bitmap
        /// </summary>
        Bitmap bitmap;
        /// <summary>
        /// The graphics
        /// </summary>
        Graphics graphics;
        /// <summary>
        /// The height
        /// </summary>
        int height;
        /// <summary>
        /// The width
        /// </summary>
        int width;

        // ******************************************** GraphicsBuffer

        /// <summary>
        /// constructor for the GraphicsBuffer
        /// </summary>
        public GraphicsBuffer ( )
            {

            width = 0;
            height = 0;
            }

        // ************************************** CreateGraphicsBuffer

        /// <summary>
        /// completes the creation of the GraphicsBuffer object
        /// </summary>
        /// <param name="width">width of the bitmap</param>
        /// <param name="height">height of the bitmap</param>
        /// <returns>true, if GraphicsBuffer created; otherwise, false</returns>
        public bool CreateGraphicsBuffer ( int width,
                                           int height )
            {
            bool  success = true;

            DeleteGraphicsBuffer ( );
            
            this.width = 0;
            this.height = 0;

            if ( ( width == 0 ) || ( height == 0 ) )
                {
                success = false;
                }
            else
                {
                this.width = width;
                this.height = height;

                bitmap = new Bitmap ( this.width, this.height );
                graphics = Graphics.FromImage ( bitmap );

                success = true;
                }

            return ( success );
            }

        // ************************************** DeleteGraphicsBuffer

        /// <summary>
        /// deletes the current GraphicsBuffer
        /// </summary>
        /// <returns>null, always</returns>
        public GraphicsBuffer DeleteGraphicsBuffer ( )
            {

            if ( graphics != null )
                {
                graphics.Dispose ( );
                graphics = null;
                }

            if ( bitmap != null )
                {
                bitmap.Dispose ( );
                bitmap = null;
                }

            width = 0;
            height = 0;
            
            return ( null );
            }

        // *************************************************** Graphic

        /// <summary>
        /// returns the current Graphic Graphics object
        /// </summary>
        /// <value>The graphic.</value>
        public Graphics Graphic
            {

            get
                {
                return ( graphics );
                }
            }

        // ************************************** RenderGraphicsBuffer

        /// <summary>
        /// Renders the buffer to the graphics object identified by
        /// graphic
        /// </summary>
        /// <param name="graphic">target graphics object (e.g., PaintEventArgs e.Graphics)</param>
        public void RenderGraphicsBuffer ( Graphics graphic )
            {

            if ( bitmap != null )
                {
                graphic.DrawImage (
                            bitmap,
                            new Rectangle ( 0, 0, width, height ),
                            new Rectangle ( 0, 0, width, height ),
                            GraphicsUnit.Pixel );
                }
            }

        } // class GraphicsBuffer

    } // namespace ZeroitSegLedLabel
