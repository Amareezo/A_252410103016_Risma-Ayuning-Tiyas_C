using System;

namespace ProgramMBG
{
    public class Sekolah
    {
        private readonly string idSekolah;
        private readonly string namaSekolah;
        private int jumlahPaketDiterima;



        public Sekolah(string idSekolah, string namaSekolah, int jumlahPaketDiterima = 0)
        {
            if (string.IsNullOrWhiteSpace(idSekolah))
                throw new ArgumentException("Id sekolah wajib diisi", nameof(idSekolah));
            if (string.IsNullOrWhiteSpace(namaSekolah))
                throw new ArgumentException("Nama sekolah wajib diisi", nameof(namaSekolah));
            if (jumlahPaketDiterima < 0)
                throw new ArgumentOutOfRangeException(nameof(jumlahPaketDiterima), "Jumlah paket tidak boleh kosong");




            this.idSekolah = idSekolah;
            this.namaSekolah = namaSekolah;
            this.jumlahPaketDiterima = jumlahPaketDiterima;
        }

        public string IdSekolah => idSekolah;
        public string NamaSekolah => namaSekolah;
        public int JumlahPaketDiterima => jumlahPaketDiterima;

        public void TampilInfo()
        {
            Console.WriteLine($"Id Sekolah: {idSekolah}");
            Console.WriteLine($"Nama Sekolah: {namaSekolah}");
            Console.WriteLine($"Jumlah Paket Diterima: {jumlahPaketDiterima}");
        }




        public void TerimaPaket(int jumlah)
        {
            if (jumlah <= 0)
                throw new ArgumentOutOfRangeException(nameof(jumlah), "Jumlah paket harus lebih dari 0");

            jumlahPaketDiterima += jumlah;
        }
    }




    public abstract class PaketMBG
    {
        private string idPaket;
        private string namaPaket;
        private int stokDapur;


        protected PaketMBG(string idPaket, string namaPaket, int stokDapur)
        {
            if (string.IsNullOrWhiteSpace(idPaket))
                throw new ArgumentException("Id paket wajib diisi", nameof(idPaket));
            if (string.IsNullOrWhiteSpace(namaPaket))
                throw new ArgumentException("Nama paket wajib diisi", nameof(namaPaket));
            if (stokDapur < 0)
                throw new ArgumentOutOfRangeException(nameof(stokDapur), "Stok dapur tidak boleh kosong");

            this.idPaket = idPaket;
            this.namaPaket = namaPaket;
            this.stokDapur = stokDapur;
        }

        public string IdPaket
        {
            get => idPaket;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Id paket wajib diisi", nameof(value));
                idPaket = value;
            }
        }





        public string NamaPaket
        {
            get => namaPaket;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nama paket wajib diisi", nameof(value));
                namaPaket = value;
            }
        }



        public int StokDapur => stokDapur;

        public bool KurangiStok(int jumlah)
        {
            if (jumlah <= 0 || jumlah > stokDapur)
                return false;

            stokDapur -= jumlah;
            return true;
        }

        public abstract int HitungBiaya(int jumlah);
    }

    public class PaketReguler : PaketMBG
    {
        public PaketReguler(string idPaket, int stokDapur)
            : base(idPaket, "Paket Reguler", stokDapur)
        {
        }

        public override int HitungBiaya(int jumlah)
        {
            if (jumlah < 0)
                throw new ArgumentOutOfRangeException(nameof(jumlah));
            return jumlah * 15000;
        }
    }




    public class PaketKhusus : PaketMBG
    {
        public PaketKhusus(string idPaket, string jenisDiet, int stokDapur)
            : base(idPaket, "Paket Khusus", stokDapur)
        {
            if (string.IsNullOrWhiteSpace(jenisDiet))
                throw new ArgumentException("Jenis diet wajib diisi.", nameof(jenisDiet));
            JenisDiet = jenisDiet;
        }

        public string JenisDiet { get; set; }

        public override int HitungBiaya(int jumlah)
        {
            if (jumlah < 0)
                // fungsinya apaan
                return jumlah * 20000;
        }
    }

    public interface DapatDidistribusikan
    {
        void KirimPaket();
    }

    public class DistribusiMBG : DapatDidistribusikan
    {
        private readonly Sekolah sekolahTujuan;
        private readonly PaketMBG paketMakanan;
        private readonly int jumlah;

        public DistribusiMBG(Sekolah sekolahTujuan, PaketMBG paketMakanan, int jumlah)
        {
            this.sekolahTujuan = //ini apaan dah
            this.paketMakanan = // lupa wak
            this.jumlah = jumlah;
        }

        public void KirimPaket()
        {
            if (jumlah <= 0)
            {
                Console.WriteLine("Tidak ada pengiriman");
                return;
            }

            if (jumlah > paketMakanan.StokDapur)
            {
                Console.WriteLine($"Pengiriman tertunda, stok habis. Stok tersedia: {paketMakanan.StokDapur} paket.");
                return;
            }

            sekolahTujuan.TerimaPaket(jumlah);
            Console.WriteLine($"Distribusi MBG berhasil! Sebanyak {jumlah} paket makanan dikirim ke sekolah {sekolahTujuan.NamaSekolah}.");
        }
    }










    public static class Program
    {
        public static void Main()
        {
            Sekolah sekolah1 = new("S001", "SMA Negeri Glenmore", 1000);
            Sekolah sekolah2 = new("S002", "MTs Negeri 11 Banyuwangi", 250);

            sekolah1.TampilInfo();
            Console.WriteLine();
            sekolah2.TampilInfo();
            Console.WriteLine();

            PaketMBG paketReguler = new PaketReguler("P001", 100000);
            PaketMBG paketKhusus = new PaketKhusus("P002", "Alergi", 70000);

            DapatDidistribusikan distribusiPertama = new DistribusiMBG(sekolah1, paketReguler, 1000);
            distribusiPertama.KirimPaket();

            DapatDidistribusikan distribusiKedua = new DistribusiMBG(sekolah2, paketKhusus, 650);
            distribusiKedua.KirimPaket();

            Console.WriteLine();
            Console.WriteLine($"Stok {paketReguler.NamaPaket}: {paketReguler.StokDapur} paket");
            Console.WriteLine($"Stok {paketKhusus.NamaPaket}: {paketKhusus.StokDapur} paket");
            Console.WriteLine();
            sekolah1.TampilInfo();
            Console.WriteLine();
            sekolah2.TampilInfo();
        }
    }
}











