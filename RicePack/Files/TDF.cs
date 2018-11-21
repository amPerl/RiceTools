using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace RicePack.Files
{
    public class TDF
    {
        private byte[] activeFileData;
        private byte[] activeFileBmp;
        private uint Flag;
        private uint Offset;
        private uint ColNum;
        private uint RowNum;
        private uint CurCol;
        private uint CurRow;
        private short vMajor;
        private short vMinor;
        private short Year;
        private byte Month;
        private byte Day;

        public static TDF Load(byte[] fileBuf, DataGridView view)
        {
            TDF tdf = new TDF();

            ushort num = BitConverter.ToUInt16(fileBuf, 2);
            tdf.activeFileData = new byte[fileBuf.Length - num];
            tdf.activeFileBmp = new byte[num];

            Array.Copy(fileBuf, tdf.activeFileBmp, num);
            Array.Copy(fileBuf, num, tdf.activeFileData, 0, tdf.activeFileData.Length);

            using (var ms = new MemoryStream(tdf.activeFileData))
            using (var br = new BinaryReader(ms))
            {
                tdf.vMajor = br.ReadInt16();
                tdf.vMinor = br.ReadInt16();
                tdf.Year = br.ReadInt16();
                tdf.Month = br.ReadByte();
                tdf.Day = br.ReadByte();
                tdf.Flag = br.ReadUInt32();
                tdf.Offset = br.ReadUInt32();
                tdf.ColNum = br.ReadUInt32();

                view.Columns.Clear();
                view.Rows.Clear();

                for (int index = 0; (long)index < (long)tdf.ColNum; ++index)
                    view.Columns.Add(new DataGridViewTextBoxColumn() { Name = index.ToString(), HeaderText = index.ToString(), SortMode = DataGridViewColumnSortMode.NotSortable });

                tdf.RowNum = br.ReadUInt32();

                for (tdf.CurRow = 0U; tdf.CurRow < tdf.RowNum; ++tdf.CurRow)
                {
                    List<object> list = new List<object>();

                    for (tdf.CurCol = 0U; tdf.CurCol < tdf.ColNum; ++tdf.CurCol)
                        list.Add(tdf.getString(br.ReadUInt32()));

                    view.Rows.Add(list.ToArray());
                }
            }

            return tdf;
        }

        public byte[] Save(DataGridView view)
        {
            byte[] filebuf;

            using (var filems = new MemoryStream())
            using (var filebw = new BinaryWriter(filems))
            {
                filebw.Write(activeFileBmp);
                int capacity = 4 * (view.Rows.Count - 1) * view.Columns.Count;
                using (var headerms = new MemoryStream(capacity))
                using (var headerbw = new BinaryWriter(headerms))
                using (var datams = new MemoryStream())
                using (var databw = new BinaryWriter(datams))
                {
                    int num = 0;
                    foreach (DataGridViewRow dataGridViewRow in view.Rows)
                    {
                        ++num;
                        if (num != view.Rows.Count)
                        {
                            foreach (DataGridViewCell dataGridViewCell in dataGridViewRow.Cells)
                            {
                                headerbw.Write((uint)((ulong)(24L + databw.BaseStream.Position) + (ulong)capacity));
                                databw.Write(Encoding.Unicode.GetBytes((dataGridViewCell.Value as string) + "\0"));
                            }
                        }
                        else
                            break;
                    }
                    filebw.Write(vMajor);
                    filebw.Write(vMinor);
                    filebw.Write(Year);
                    filebw.Write(Month);
                    filebw.Write(Day);
                    filebw.Write(Flag);
                    filebw.Write((uint)(24 + capacity + databw.BaseStream.Position));
                    filebw.Write(ColNum);
                    filebw.Write((uint)(view.Rows.Count - 1));
                    filebw.Write(headerms.ToArray());
                    filebw.Write(datams.ToArray());
                }
                filebuf = filems.ToArray();
            }

            return filebuf;
        }

        private string getString(uint offset)
        {
            int num = 0;

            while (num < activeFileData.Length - offset && (activeFileData[offset + num * 2] != 0 || activeFileData[offset + num * 2 + 1L] != 0))
                ++num;

            return Encoding.Unicode.GetString(activeFileData, (int)offset, num * 2);
        }
    }
}
