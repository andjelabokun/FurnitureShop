import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import api from '../services/api';
import { korpa } from './Cart';

function ProductDetails() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [proizvod, setProizvod] = useState(null);
  const [boje, setBoje] = useState([]);
  const [materijali, setMaterijali] = useState([]);
  const [dimenzije, setDimenzije] = useState([]);
  const [proizvodjaci, setProizvodjaci] = useState([]);
  const [podkategorije, setPodkategorije] = useState([]);
  const [kategorije, setKategorije] = useState([]);

  const [loading, setLoading] = useState(true);
  const [greska, setGreska] = useState('');
  const [poruka, setPoruka] = useState('');
  const [tipPoruke, setTipPoruke] = useState('success');

  const backendUrl = api.defaults.baseURL
    ? api.defaults.baseURL.replace(/\/api\/?$/, '')
    : 'https://localhost:7267';

  useEffect(() => {
    ucitajSve();
  }, [id]);

  const korisnikJePrijavljen = () => {
    return Boolean(localStorage.getItem('token'));
  };

  const getVrednost = (obj, keys, fallback = '') => {
    for (const key of keys) {
      if (obj && obj[key] !== undefined && obj[key] !== null) {
        return obj[key];
      }
    }

    return fallback;
  };

  const getId = (obj, keys) => {
    for (const key of keys) {
      if (obj && obj[key] !== undefined && obj[key] !== null) {
        return obj[key];
      }
    }

    return '';
  };

  const getNaziv = (obj) => {
    return obj?.naziv ?? obj?.Naziv ?? obj?.ime ?? obj?.Ime ?? '';
  };

  const formatirajSlikaUrl = (url) => {
    if (!url) return '';

    if (url.startsWith('http') || url.startsWith('blob:')) {
      return url;
    }

    if (url.startsWith('/')) {
      return `${backendUrl}${url}`;
    }

    return `${backendUrl}/${url}`;
  };

  const ucitajSaMogucimRutama = async (rute) => {
    for (const ruta of rute) {
      try {
        const res = await api.get(ruta);
        return Array.isArray(res.data) ? res.data : [];
      } catch (err) {
        console.log(`Ne radi ruta ${ruta}:`, err.response?.status);
      }
    }

    return [];
  };

  const ucitajSve = async () => {
    try {
      setLoading(true);

      const proizvodRes = await api.get(`/Proizvodi/${id}`);

      const [
        bojeData,
        materijaliData,
        dimenzijeData,
        proizvodjaciData,
        podkategorijeData,
        kategorijeData
      ] = await Promise.all([
        ucitajSaMogucimRutama(['/Boje']),
        ucitajSaMogucimRutama(['/Materijali', '/Materijal']),
        ucitajSaMogucimRutama(['/Dimenzije']),
        ucitajSaMogucimRutama(['/Proizvodjaci', '/Proizvodjac']),
        ucitajSaMogucimRutama(['/PodKategorije', '/Podkategorije', '/Podkategorija']),
        ucitajSaMogucimRutama(['/Kategorije'])
      ]);

      setProizvod(proizvodRes.data);
      setBoje(bojeData);
      setMaterijali(materijaliData);
      setDimenzije(dimenzijeData);
      setProizvodjaci(proizvodjaciData);
      setPodkategorije(podkategorijeData);
      setKategorije(kategorijeData);
    } catch (err) {
      console.log('Greška pri učitavanju proizvoda:', err.response?.data || err.message);
      setGreska('Proizvod nije pronađen.');
    } finally {
      setLoading(false);
    }
  };

  const formatCena = (cena) => {
    return Number(cena || 0).toLocaleString('sr-RS');
  };

  const prikaziBool = (vrednost) => {
    return vrednost ? 'Da' : 'Ne';
  };

  const nadjiBoju = () => {
    const bojaId = getVrednost(proizvod, ['bojaId', 'BojaId', 'bojaID', 'BojaID']);
    return boje.find(b =>
      String(getId(b, ['bojaID', 'bojaId', 'BojaID', 'BojaId', 'id', 'Id'])) === String(bojaId)
    );
  };

  const nadjiMaterijal = () => {
    const materijalId = getVrednost(proizvod, ['materijalId', 'MaterijalId', 'materijalID', 'MaterijalID']);
    return materijali.find(m =>
      String(getId(m, ['materijalID', 'materijalId', 'MaterijalID', 'MaterijalId', 'id', 'Id'])) === String(materijalId)
    );
  };

  const nadjiDimenzije = () => {
    const dimenzijeId = getVrednost(proizvod, ['dimenzijeId', 'DimenzijeId', 'dimenzijeID', 'DimenzijeID']);
    return dimenzije.find(d =>
      String(getId(d, ['dimenzijeID', 'dimenzijeId', 'DimenzijeID', 'DimenzijeId', 'id', 'Id'])) === String(dimenzijeId)
    );
  };

  const nadjiProizvodjaca = () => {
    const proizvodjacId = getVrednost(proizvod, ['proizvodjacId', 'ProizvodjacId', 'proizvodjacID', 'ProizvodjacID']);
    return proizvodjaci.find(p =>
      String(getId(p, ['proizvodjacID', 'proizvodjacId', 'ProizvodjacID', 'ProizvodjacId', 'id', 'Id'])) === String(proizvodjacId)
    );
  };

  const nadjiPodkategoriju = () => {
    const podkategorijaId = getVrednost(proizvod, [
      'podkategorijaId',
      'PodkategorijaId',
      'podkategorijaID',
      'PodkategorijaID',
      'podKategorijaId',
      'PodKategorijaId',
      'podKategorijaID',
      'PodKategorijaID'
    ]);

    return podkategorije.find(p =>
      String(getId(p, [
        'podkategorijaID',
        'podkategorijaId',
        'PodkategorijaID',
        'PodkategorijaId',
        'podKategorijaID',
        'podKategorijaId',
        'PodKategorijaID',
        'PodKategorijaId',
        'id',
        'Id'
      ])) === String(podkategorijaId)
    );
  };

  const nadjiKategoriju = () => {
    const podkategorija = nadjiPodkategoriju();

    if (!podkategorija) return null;

    const kategorijaId = getVrednost(podkategorija, [
      'kategorijaId',
      'kategorijaID',
      'KategorijaId',
      'KategorijaID'
    ]);

    return kategorije.find(k =>
      String(getId(k, ['kategorijaID', 'kategorijaId', 'KategorijaID', 'KategorijaId', 'id', 'Id'])) === String(kategorijaId)
    );
  };

  const getDimenzijeTekst = () => {
    const sirinaIzDto = getVrednost(proizvod, ['sirina', 'Sirina'], null);
    const visinaIzDto = getVrednost(proizvod, ['visina', 'Visina'], null);
    const dubinaIzDto = getVrednost(proizvod, ['dubina', 'Dubina'], null);

    if (sirinaIzDto !== null && visinaIzDto !== null && dubinaIzDto !== null) {
      return `${sirinaIzDto} x ${visinaIzDto} x ${dubinaIzDto} cm`;
    }

    const d = nadjiDimenzije();

    if (!d) return '-';

    const naziv = getNaziv(d);
    if (naziv) return naziv;

    const sirina = getVrednost(d, ['sirina', 'Sirina'], '');
    const visina = getVrednost(d, ['visina', 'Visina'], '');
    const dubina = getVrednost(d, ['dubina', 'Dubina'], '');

    return `${sirina} x ${visina} x ${dubina} cm`;
  };

  const dodajUKorpu = () => {
    if (!proizvod) return;

    if (!korisnikJePrijavljen()) {
      setTipPoruke('error');
      setPoruka('Morate biti prijavljeni da biste dodali proizvod u korpu.');

      setTimeout(() => {
        navigate('/login');
      }, 1200);

      return;
    }

    const proizvodID = Number(
      getVrednost(proizvod, [
        'proizvodID',
        'ProizvodID',
        'proizvodId',
        'ProizvodId',
        'id',
        'Id'
      ])
    );

    const naziv = getVrednost(proizvod, ['naziv', 'Naziv']);
    const cena = Number(getVrednost(proizvod, ['cena', 'Cena'], 0));
    const slikaUrl = getVrednost(proizvod, ['slikaUrl', 'SlikaUrl']);

    korpa.addItem({
      proizvodID,
      naziv,
      cena,
      slikaUrl: slikaUrl ? formatirajSlikaUrl(slikaUrl) : '',
    });

    setTipPoruke('success');
    setPoruka('Proizvod je dodat u korpu.');

    setTimeout(() => {
      navigate('/cart');
    }, 700);
  };

  const renderSpecijalizacija = () => {
    const tip = getVrednost(proizvod, ['tipProizvoda', 'TipProizvoda'], 'Proizvod');

    if (tip === 'Garnitura') {
      return (
        <div style={styles.infoBox}>
          <h3 style={styles.infoTitle}>Detalji garniture</h3>
          <Info label="Punjenje" value={getVrednost(proizvod, ['punjenje', 'Punjenje'], '-')} />
          <Info label="Orijentacija" value={getVrednost(proizvod, ['orijentacija', 'Orijentacija'], '-')} />
          <Info label="Broj mesta" value={getVrednost(proizvod, ['brojMesta', 'BrojMesta'], '-')} />
          <Info label="Rasklopiva" value={prikaziBool(getVrednost(proizvod, ['rasklopiva', 'Rasklopiva'], false))} />
        </div>
      );
    }

    if (tip === 'Krevet') {
      return (
        <div style={styles.infoBox}>
          <h3 style={styles.infoTitle}>Detalji kreveta</h3>
          <Info label="Dimenzija dušeka" value={getVrednost(proizvod, ['dimenzijaDuseka', 'DimenzijaDuseka'], '-')} />
          <Info label="Tip kreveta" value={getVrednost(proizvod, ['tipKreveta', 'TipKreveta'], '-')} />
          <Info label="Ima sanduk" value={prikaziBool(getVrednost(proizvod, ['imaSanduk', 'ImaSanduk'], false))} />
        </div>
      );
    }

    if (tip === 'Orman') {
      return (
        <div style={styles.infoBox}>
          <h3 style={styles.infoTitle}>Detalji ormara</h3>
          <Info label="Broj vrata" value={getVrednost(proizvod, ['brojVrata', 'BrojVrata'], '-')} />
          <Info label="Tip vrata" value={getVrednost(proizvod, ['tipVrata', 'TipVrata'], '-')} />
          <Info label="Ima ogledalo" value={prikaziBool(getVrednost(proizvod, ['imaOgledalo', 'ImaOgledalo'], false))} />
        </div>
      );
    }

    if (tip === 'Sto') {
      return (
        <div style={styles.infoBox}>
          <h3 style={styles.infoTitle}>Detalji stola</h3>
          <Info label="Oblik" value={getVrednost(proizvod, ['oblik', 'Oblik'], '-')} />
          <Info label="Broj mesta" value={getVrednost(proizvod, ['brojMesta', 'BrojMesta'], '-')} />
          <Info label="Rasklopiv" value={prikaziBool(getVrednost(proizvod, ['rasklopiv', 'Rasklopiv'], false))} />
        </div>
      );
    }

    return null;
  };

  if (loading) {
    return (
      <main style={styles.page}>
        <p style={styles.loading}>Učitavanje proizvoda...</p>
      </main>
    );
  }

  if (greska || !proizvod) {
    return (
      <main style={styles.page}>
        <p style={styles.greska}>{greska}</p>
        <button style={styles.backBtn} onClick={() => navigate('/products')}>
          Nazad na proizvode
        </button>
      </main>
    );
  }

  const naziv = getVrednost(proizvod, ['naziv', 'Naziv']);
  const opis = getVrednost(proizvod, ['opis', 'Opis']);
  const cena = getVrednost(proizvod, ['cena', 'Cena'], 0);
  const stanje = getVrednost(proizvod, ['stanjeNaLageru', 'StanjeNaLageru'], 0);
  const slikaUrl = getVrednost(proizvod, ['slikaUrl', 'SlikaUrl']);
  const tip = getVrednost(proizvod, ['tipProizvoda', 'TipProizvoda'], 'Proizvod');

  const boja = nadjiBoju();
  const materijal = nadjiMaterijal();
  const proizvodjac = nadjiProizvodjaca();
  const podkategorija = nadjiPodkategoriju();
  const kategorija = nadjiKategoriju();

  return (
    <main style={styles.page}>
      <button style={styles.backBtn} onClick={() => navigate(-1)}>
        ← Nazad
      </button>

      {poruka && (
        <div
          style={{
            ...styles.poruka,
            ...(tipPoruke === 'error' ? styles.porukaError : {})
          }}
        >
          {poruka}
        </div>
      )}

      <section style={styles.card}>
        <div style={styles.imageBox}>
          {slikaUrl ? (
            <img
              src={formatirajSlikaUrl(slikaUrl)}
              alt={naziv}
              style={styles.image}
            />
          ) : (
            <div style={styles.noImage}>
              {naziv}
            </div>
          )}
        </div>

        <div style={styles.content}>
          <span style={styles.tipBadge}>{tip}</span>

          <h1 style={styles.title}>{naziv}</h1>

          <p style={styles.opis}>{opis}</p>

          <p style={styles.cena}>{formatCena(cena)} RSD</p>

          <button
            style={{
              ...styles.cartBtn,
              ...(Number(stanje) <= 0 ? styles.cartBtnDisabled : {})
            }}
            onClick={dodajUKorpu}
            disabled={Number(stanje) <= 0}
          >
            {Number(stanje) <= 0 ? 'Nema na stanju' : 'Dodaj u korpu'}
          </button>

          <div style={styles.infoBox}>
            <h3 style={styles.infoTitle}>Osnovne informacije</h3>

            <Info label="Stanje na lageru" value={`${stanje} kom`} />
            <Info label="Kategorija" value={kategorija ? getNaziv(kategorija) : '-'} />
            <Info label="Podkategorija" value={podkategorija ? getNaziv(podkategorija) : '-'} />
            <Info label="Boja" value={boja ? getNaziv(boja) : '-'} />
            <Info label="Materijal" value={materijal ? getNaziv(materijal) : '-'} />
            <Info label="Proizvođač" value={proizvodjac ? getNaziv(proizvodjac) : '-'} />
            <Info label="Dimenzije" value={getDimenzijeTekst()} />
            <Info label="ID proizvoda" value={`#${getVrednost(proizvod, ['proizvodID', 'ProizvodID'])}`} />
          </div>

          {renderSpecijalizacija()}
        </div>
      </section>
    </main>
  );
}

function Info({ label, value }) {
  return (
    <div style={styles.infoRow}>
      <span style={styles.infoLabel}>{label}</span>
      <span style={styles.infoValue}>{value}</span>
    </div>
  );
}

const styles = {
  page: {
    minHeight: '100vh',
    padding: '50px 80px',
    background: 'linear-gradient(180deg, #f7fbff 0%, #ffffff 100%)',
  },
  backBtn: {
    marginBottom: '25px',
    padding: '10px 18px',
    borderRadius: '8px',
    border: '1px solid #102a43',
    backgroundColor: 'white',
    color: '#102a43',
    fontWeight: '700',
    cursor: 'pointer',
  },
  poruka: {
    backgroundColor: '#dcfce7',
    color: '#166534',
    padding: '12px 18px',
    borderRadius: '10px',
    marginBottom: '20px',
    fontWeight: '700',
  },
  porukaError: {
    backgroundColor: '#fee2e2',
    color: '#991b1b',
  },
  card: {
    display: 'grid',
    gridTemplateColumns: '1.1fr 1fr',
    gap: '45px',
    backgroundColor: 'white',
    borderRadius: '18px',
    padding: '35px',
    boxShadow: '0 6px 25px rgba(0,0,0,0.08)',
  },
  imageBox: {
    width: '100%',
    minHeight: '520px',
    borderRadius: '16px',
    overflow: 'hidden',
    backgroundColor: '#d9ecff',
  },
  image: {
    width: '100%',
    height: '520px',
    objectFit: 'cover',
    display: 'block',
  },
  noImage: {
    width: '100%',
    height: '520px',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    padding: '30px',
    textAlign: 'center',
    backgroundColor: '#d9ecff',
    color: '#00449e',
    fontSize: '28px',
    fontWeight: '800',
  },
  content: {
    display: 'flex',
    flexDirection: 'column',
    gap: '18px',
  },
  tipBadge: {
    alignSelf: 'flex-start',
    backgroundColor: '#dbeafe',
    color: '#1e40af',
    padding: '7px 14px',
    borderRadius: '999px',
    fontSize: '14px',
    fontWeight: '700',
  },
  title: {
    margin: 0,
    fontSize: '36px',
    color: '#102a43',
  },
  opis: {
    margin: 0,
    fontSize: '18px',
    lineHeight: '1.6',
    color: '#486581',
  },
  cena: {
    margin: '5px 0',
    fontSize: '32px',
    fontWeight: '800',
    color: '#00449e',
  },
  cartBtn: {
    padding: '16px 22px',
    borderRadius: '10px',
    border: 'none',
    backgroundColor: '#102a43',
    color: 'white',
    fontSize: '17px',
    fontWeight: '800',
    cursor: 'pointer',
  },
  cartBtnDisabled: {
    backgroundColor: '#9ca3af',
    cursor: 'not-allowed',
  },
  infoBox: {
    backgroundColor: '#f7fbff',
    border: '1px solid #d9ecff',
    borderRadius: '14px',
    padding: '20px',
  },
  infoTitle: {
    margin: '0 0 14px 0',
    color: '#102a43',
    fontSize: '20px',
  },
  infoRow: {
    display: 'flex',
    justifyContent: 'space-between',
    gap: '20px',
    padding: '10px 0',
    borderBottom: '1px solid #e0e8f0',
  },
  infoLabel: {
    color: '#627d98',
    fontWeight: '700',
  },
  infoValue: {
    color: '#102a43',
    fontWeight: '700',
    textAlign: 'right',
  },
  loading: {
    fontSize: '20px',
    color: '#102a43',
  },
  greska: {
    fontSize: '20px',
    color: '#dc2626',
  },
};

export default ProductDetails;