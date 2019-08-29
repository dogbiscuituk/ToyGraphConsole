using System;
using ToyGraf.Engine.Types;

namespace ToyGraf.Engine.Utility
{
    public static class Grids
    {
        /// <summary>
        /// Get the coordinates of all points in a regular 3D xyz lattice, where -1 <= x,y,z <= +1.
        /// For full details, please refer to GetGrid(int, int, int).
        /// </summary>
        /// <param name="p">The number of steps along the x/y/x axes.</param>
        /// <returns>
        /// 3(cx+1)(cy+1)(cz+1) floats, being the xyz coordinates of the points in the lattice.
        /// </returns>
        public static float[] GetGrid(Point3 p) => GetGrid(p.X, p.Y, p.Z);

        /// <summary>
        /// Get the coordinates of all points in a regular 3D xyz lattice, where -1 <= x,y,z <= +1.
        /// Points are returned ordered by x value, then by y value, and finally by z value.
        /// In other words, x varies most slowly, and z most quickly.
        /// To get the points on a regular 2D grid, set the missing axis strip count to 0.
        /// For example, GetVertexCoords(8, 8, 0) returns the 81 vertices of an 8x8, xy chessboard.
        /// To get the points along a single axis, set both missing axes' strip counts to 0.
        /// For example, GetVertexCoords(8, 0, 0) returns 9 points evenly spaced along the x axis.
        /// </summary>
        /// <param name="cx">The number of steps along the x axis.</param>
        /// <param name="cy">The number of steps along the y axis.</param>
        /// <param name="cz">The number of steps along the z axis.</param>
        /// <returns>
        /// 3(cx+1)(cy+1)(cz+1) floats, being the xyz coordinates of the points in the lattice.
        /// </returns>
        public static float[] GetGrid(int cx = 0, int cy = 0, int cz = 0)
        {
            var result = new float[3 * (cx + 1) * (cy + 1) * (cz + 1)];
            var p = 0;
            for (var i = 0; i <= cx; i++)
            {
                var x = cx == 0 ? 0 : 2f * i / cx - 1;
                for (int j = 0; j <= cy; j++)
                {
                    var y = cy == 0 ? 0 : 2f * j / cy - 1;
                    for (int k = 0; k <= cz; k++)
                    {
                        var z = cz == 0 ? 0: 2f * k / cz - 1;
                        result[p++] = x;
                        result[p++] = y;
                        result[p++] = z;
                    }
                }
            }
            return result;
        }

        public static int[] GetIndices(Point3 p, Pattern pattern)
        {
            switch (pattern)
            {
                case Pattern.None:
                    return new int[0];
                case Pattern.LinesStrip:
                    return GetLineIndicesX(p.X);
                case Pattern.Triangles:
                    return GetTriangleIndicesXY(p.X, p.Y);
                case Pattern.TriangleStrip:
                    return GetTriangleStripIndicesXY(p.X, p.Y);
            }
            return new int[0];
        }

        public static int[] GetLineIndicesX(int cx)
        {
            var result = new int[cx + 1];
            for (var i = 0; i <= cx; i++)
                result[i] = i;
            return result;
        }

        /// <summary>
        /// Get the order of vertices required to draw triangles covering the grid.
        /// This method uses the vertex return order implemented by GetVertexCoords.
        /// </summary>
        /// <param name="cx">The number of steps along the x axis.</param>
        /// <param name="cy">The number of steps along the y axis.</param>
        /// A total of 6*cx*cy ints, ranging from 0 to 3(cx+1)(cy+1)-1 inclusive. These are the
        /// required vertex indices. Note that these must be multiplied by 3 to index the float data
        /// returned by GetVertexCoords, as every such vertex is postprocessed to add a z coordinate
        /// of zero, and so eventually comprises 3 floats in total.
        public static int[] GetTriangleIndicesXY(int cx, int cy)
        {
            var result = new int[6 * cx * cy];
            for (int i = 0, p = 0, q = 0; i < cx; i++, q++)
                for (var j = 0; j < cy; j++)
                {
                    result[p++] = q;
                    result[p++] = q++ + cy + 1;
                    result[p++] = q;
                    result[p++] = q;
                    result[p++] = q + cy;
                    result[p++] = q + cy + 1;
                }
            return result;
        }

        /// <summary>
        /// Get the order of vertices required to draw a single continuous triangle strip covering
        /// the grid. This method uses the vertex return order implemented by GetVertexCoords, and
        /// the output is a sequence of alternately ascending and descending strips. For the 6x3
        /// example below (cx=5, cy=2), the 26-element returned sequence is:
        /// 
        ///     00 - 03-01-04-02-05 - 08-04-07-03-06
        ///        - 09-07-10-08-11 - 14-10-03-09-12
        ///        - 15-13-16-14-17
        ///        
        /// To see this, play Snakes & Ladders. Start in the bottom left corner 00, and climb first
        /// up the ladder 03-01-04-02-05, then down the snake 08-04-07-03-06. Now climb back up the
        /// ladder 09-07-10-08-11, and down 14-10-13-09-12. Lastly climb 15-13-16-14-17 and declare
        /// victory. The result describes a single triangle strip, covering the entire grid, though
        /// with degenerate or "null" triangles at 02-05-08, 03-06-09, 08-11-14, and 09-12-15. This
        /// pattern has cx-2 such triangles, so a 1001x1001 grid (cx=cy=1000) will have more than 2
        /// million triangles, less than a thousand of which are degenerate. Hence, any performance
        /// improvement from further grid optimization will be limited to less than 0.05%.
        /// 
        ///    y
        ///     |
        ///    2|   02--05--08--11--14--17
        ///     |     \    /  \    /  \
        ///     |      \  /    \  /    \
        ///    1|   01--04--07--10--13--16
        ///     |     \    /  \    /  \
        ///     |      \  /    \  /    \
        ///    0|   00--03--06--09--12--15
        ///    
        ///         0---1---2---3---4---5--- x
        /// 
        /// </summary>
        /// <param name="cx">The number of steps along the x axis.</param>
        /// <param name="cy">The number of steps along the y axis.</param>
        /// <returns>
        /// A total of cx(2cy+1)+1 ints, ranging from 0 to 3(cx+1)(cy+1)-1 inclusive. These are the
        /// required vertex indices. Note that these must be multiplied by 3 to index the float data
        /// returned by GetVertexCoords, as every such vertex is postprocessed to add a z coordinate
        /// of zero, and so eventually comprises 3 floats in total.
        /// </returns>
        public static int[] GetTriangleStripIndicesXY(int cx, int cy)
        {
            var result = new int[cx * (2 * cy + 1) + 1];
            int p = 0, q = 0;
            result[p++] = 0;
            for (var i = 0; i < cx; i++)
            {
                for (var j = 0; j < cy; j++)
                {
                    result[p++] = q + cy + 1;
                    result[p++] = (i & 1) == 0 ? ++q : --q;
                }
                result[p++] = q += cy + 1;
            }
            return result;
        }
    }
}
