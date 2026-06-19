import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import api from '../services/api';

function DetaljiPorudzbine() {
  const { id } = useParams();
  const navigate = useNavigate();
  const { isAdmin } = useAuth();

  const [porudzbina, setPorudzbina] = useState(null);
  const [proizvodi, setProizvodi] = useState([]);
  const [loading, setLoading] = useState(true);
  const [poruka, setPoruka] = useState('');

  const backendUrl = api.defaults.baseURL
    ? api.defaults.baseURL.replace(/\/api\/?$/, '')
    : 'https://localhost:7267';

  useEffect(() => {
    if (!isAdmin()) {
      navigate('/');
      return;
    }

    ucitajPodatke();
  }, [id]);

  const getId = (obj, keys) => {
    for (const key of keys) {
      if (obj && obj[key] !== undefined && obj[key] !== null) {
        return obj[key];
      }
    }
    return '';
  };

  const getVrednost = (obj, keys, fallback = '') => {
    for (const key of keys) {
      if (obj && obj[key] !== undefined && obj[key] !== null) {
        return obj[key];
      }
    }
    return fallback;
  };

  const getPorudzbinaId = (p) => {
    return getId(p, [
      'porudzbinaID',
      'porudzbinaId',
      'PorudzbinaID',
      'PorudzbinaId',
      'id',
      'Id'
    ]);
  };

  const getKupacObj = (p) => {
    return p?.kupac || p?.Kupac || p?.korisnik || p?.Korisnik || p?.user || p?.User || null;
  };

  const getKupacIme = (p) => {
    const kupac = getKupacObj(p);

    return (
      getVrednost(p, ['kupacIme', 'KupacIme', 'imeKupca', 'ImeKupca']) ||
      getVrednost(kupac, ['ime', 'Ime']) ||
      ''
    );
  };

  const getKupacPrezime = (p) => {
    const kupac = getKupacObj(p);

    return (
      getVrednost(p, ['kupacPrezime', 'KupacPrezime', 'prezimeKupca', 'PrezimeKupca']) ||
      getVrednost(kupac, ['prezime', 'Prezime']) ||
      ''
    );
  };

  const getKupacEmail = (p) => {
    const kupac = getKupacObj(p);

    return (
      getVrednost(p, ['kupacEmail', 'KupacEmail', 'emailKupca', 'EmailKupca']) ||
      getVrednost(kupac, ['email', 'Email', 'userName', 'UserName']) ||
      ''
    );
  };

  const getKupacTelefon = (p) => {
    const kupac = getKupacObj(p);

    return (
      getVrednost(p, ['kupacTelefon', 'KupacTelefon', 'telefonKupca', 'TelefonKupca']) ||
      getVrednost(kupac, ['telefon', 'Telefon', 'phoneNumber', 'PhoneNumber']) ||
      ''
    );
  };

  const getKupacAdresa = (p) => {
    const kupac = getKupacObj(p);

    return (
      getVrednost(p, [
        'adresa',
        'Adresa',
        'adresaIsporuke',
        'AdresaIsporuke',
        'adresaKupca',
        'AdresaKupca'
      ]) ||
      getVrednost(kupac, ['adresaIsporuke', 'AdresaIsporuke', 'adresa', 'Adresa']) ||
      ''
    );
  };

  const getStavke = (p) => {
    const stavke = getVrednost(p, [
      'stavke',
      'Stavke',
      'stavkePorudzbine',
      'StavkePorudzbine'
    ]);

    return Array.isArray(stavke) ? stavke : [];
  };

  const getNaziv = (obj) => {
    return obj?.naziv ?? obj?.Naziv ?? obj?.ime ?? obj?.Ime ?? '';
  };

  const getProizvodId = (p) => {
    return getId(p, [
      'proizvodID',
      'proizvodId',
      'ProizvodID',
      'ProizvodId',
      'id',
      'Id'
    ]);
  };

  const getStavkaProizvodId = (stavka) => {
    return getId(stavka, [
      'proizvodID',
      'proizvodId',
      'ProizvodID',
      'ProizvodId'
    ]);
  };

  const getStavkaProizvod = (stavka) => {
    const proizvodIzStavke = getVrednost(stavka, ['proizvod', 'Proizvod'], null);
    if (proizvodIzStavke) return proizvodIzStavke;

    const proizvodId = getStavkaProizvodId(stavka);

    return proizvodi.find(p => String(getProizvodId(p)) === String(proizvodId)) || null;
  };

  const getStavkaNaziv = (stavka) => {
    const proizvod = getStavkaProizvod(stavka);
    const proizvodId = getStavkaProizvodId(stavka);

    return (
      getVrednost(stavka, [
        'proizvodNaziv',
        'ProizvodNaziv',
        'nazivProizvoda',
        'NazivProizvoda',
        'naziv',
        'Naziv'
      ]) ||
      getNaziv(proizvod) ||
      (proizvodId ? `Proizvod #${proizvodId}` : 'Proizvod')
    );
  };

  const getStavkaKolicina = (stavka) => {
    return getVrednost(stavka, ['kolicina', 'Kolicina'], 1);
  };

  const getStavkaCena = (stavka) => {
    return getVrednost(stavka, [
      'cena',
      'Cena',
      'cenaPoKomadu',
      'CenaPoKomadu',
      'jedinicnaCena',
      'JedinicnaCena'
    ], '');
  };

  const getStavkaIznos = (stavka) => {
    const iznos = getVrednost(stavka, ['iznos', 'Iznos'], '');

    if (iznos !== '' && iznos !== null && iznos !== undefined) {
      return iznos;
    }

    const cena = getStavkaCena(stavka);
    const kolicina = getStavkaKolicina(stavka);

    if (cena !== '' && cena !== null && cena !== undefined) {
      return Number(cena) * Number(kolicina || 1);
    }

    return '';
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

  const getSlikaUrl = (obj) => {
    return getVrednost(obj, [
      'slikaUrl',
      'SlikaUrl',
      'slikaURL',
      'SlikaURL'
    ]);
  };

  const ucitajPodatke = async () => {
    setLoading(true);
    setPoruka('');

    try {
      let pronadjenaPorudzbina = null;

      try {
        const res = await api.get(`/Porudzbine/${id}`);
        pronadjenaPorudzbina = res.data;
      } catch (err) {
        console.log('GET po ID-u nije uspeo, pokušavam preko liste:', err.response?.data || err.message);
      }

      const sviRes = await api.get('/Porudzbine');
      const svePorudzbine = Array.isArray(sviRes.data) ? sviRes.data : [];
      const porudzbinaIzListe = svePorudzbine.find(p => String(getPorudzbinaId(p)) === String(id));

      if (!pronadjenaPorudzbina) {
        pronadjenaPorudzbina = porudzbinaIzListe;
      } else if (porudzbinaIzListe && getStavke(pronadjenaPorudzbina).length === 0) {
        pronadjenaPorudzbina = {
          ...pronadjenaPorudzbina,
          ...porudzbinaIzListe
        };
      }

      if (!pronadjenaPorudzbina) {
        setPoruka('Porudžbina nije pronađena.');
      }

      setPorudzbina(pronadjenaPorudzbina || null);

      try {
        const proizvodiRes = await api.get('/Proizvodi');
        setProizvodi(Array.isArray(proizvodiRes.data) ? proizvodiRes.data : []);
      } catch (err) {
        console.log('Nije moguće učitati proizvode:', err.response?.data || err.message);
      }
    } catch (err) {
      console.log('Greška pri učitavanju detalja porudžbine:', err.response?.data || err.message);
      setPoruka('Greška pri učitavanju detalja porudžbine.');
    } finally {
      setLoading(false);
    }
  };

  const promeniStatus = async (noviStatus) => {
    if (!porudzbina) return;

    const ukupanIznos = getVrednost(porudzbina, ['ukupanIznos', 'UkupanIznos'], 0);

    try {
      await api.put(`/Porudzbine/${id}/status`, {
        status: noviStatus,
        ukupanIznos: Number(ukupanIznos) || 0
      });

      setPoruka('Status uspešno promenjen.');
      await ucitajPodatke();
      setTimeout(() => setPoruka(''), 3000);
    } catch (err) {
      console.log('Greška pri promeni statusa:', err.response?.data || err.message);
      setPoruka('Greška pri promeni statusa porudžbine.');
      setTimeout(() => setPoruka(''), 5000);
    }
  };

  const statusBoja = (status) => {
    switch (status) {
      case 'Kreirana': return '#fef3c7';
      case 'U obradi': return '#dbeafe';
      case 'Isporucena': return '#dcfce7';
      case 'Otkazana': return '#fee2e2';
      default: return '#f3f4f6';
    }
  };

  const statusTextBoja = (status) => {
    switch (status) {
      case 'Kreirana': return '#92400e';
      case 'U obradi': return '#1e40af';
      case 'Isporucena': return '#166534';
      case 'Otkazana': return '#991b1b';
      default: return '#374151';
    }
  };

  if (loading) {
    return (
      <main style={styles.page}>
        <p style={styles.loading}>Učitavanje porudžbine...</p>
      </main>
    );
  }

  if (!porudzbina) {
    return (
      <main style={styles.page}>
        <button style={styles.backBtn} onClick={() => navigate('/admin/porudzbine')}>
          ← Nazad na porudžbine
        </button>
        <div style={styles.porukaGreska}>{poruka || 'Porudžbina nije pronađena.'}</div>
      </main>
    );
  }

  const porudzbinaId = getPorudzbinaId(porudzbina);
  const datumVreme = getVrednost(porudzbina, ['datumVreme', 'DatumVreme', 'datum', 'Datum']);
  const ukupanIznos = getVrednost(porudzbina, ['ukupanIznos', 'UkupanIznos'], 0);
  const status = getVrednost(porudzbina, ['status', 'Status'], 'Kreirana');
  const stavke = getStavke(porudzbina);
  const kupac = `${getKupacIme(porudzbina)} ${getKupacPrezime(porudzbina)}`.trim();

  return (
    <main style={styles.page}>
      <button style={styles.backBtn} onClick={() => navigate('/admin/porudzbine')}>
        ← Nazad na porudžbine
      </button>

      <div style={styles.headerCard}>
        <div>
          <h1 style={styles.title}>Porudžbina #{porudzbinaId}</h1>
          <p style={styles.subtitle}>Detaljan pregled kupca, dostave i stavki porudžbine.</p>
        </div>

        <span
          style={{
            ...styles.statusBadgeVeliki,
            backgroundColor: statusBoja(status),
            color: statusTextBoja(status)
          }}
        >
          {status}
        </span>
      </div>

      {poruka && <div style={styles.poruka}>{poruka}</div>}

      <section style={styles.grid}>
        <div style={styles.card}>
          <h2 style={styles.cardTitle}>Podaci o porudžbini</h2>
          <Info label="ID" value={`#${porudzbinaId}`} />
          <Info label="Datum" value={datumVreme ? new Date(datumVreme).toLocaleString('sr-RS') : '-'} />
          <Info label="Ukupan iznos" value={`${Number(ukupanIznos).toLocaleString()} RSD`} />

          <label style={styles.label}>Promeni status</label>
          <select
            style={styles.input}
            value={status}
            onChange={(e) => promeniStatus(e.target.value)}
          >
            <option value="Kreirana">Kreirana</option>
            <option value="U obradi">U obradi</option>
            <option value="Isporucena">Isporucena</option>
            <option value="Otkazana">Otkazana</option>
          </select>
        </div>

        <div style={styles.card}>
          <h2 style={styles.cardTitle}>Podaci o kupcu</h2>
          <Info label="Kupac" value={kupac || '-'} />
          <Info label="Email" value={getKupacEmail(porudzbina) || '-'} />
          <Info label="Telefon" value={getKupacTelefon(porudzbina) || '-'} />
          <Info label="Adresa" value={getKupacAdresa(porudzbina) || '-'} />
        </div>
      </section>

      <section style={styles.stavkeCard}>
        <div style={styles.stavkeHeader}>
          <h2 style={styles.cardTitle}>Stavke porudžbine</h2>
          <span style={styles.stavkeCount}>{stavke.length} stavki</span>
        </div>

        {stavke.length > 0 ? (
          <table style={styles.table}>
            <thead>
              <tr style={styles.thead}>
                <th style={styles.th}>Proizvod</th>
                <th style={styles.th}>ID proizvoda</th>
                <th style={styles.th}>Količina</th>
                <th style={styles.th}>Cena po komadu</th>
                <th style={styles.th}>Iznos</th>
              </tr>
            </thead>

            <tbody>
              {stavke.map((stavka, index) => {
                const proizvod = getStavkaProizvod(stavka);
                const slikaUrl = getSlikaUrl(proizvod);
                const proizvodId = getStavkaProizvodId(stavka);
                const kolicina = getStavkaKolicina(stavka);
                const cena = getStavkaCena(stavka);
                const iznos = getStavkaIznos(stavka);

                return (
                  <tr key={index} style={styles.tr}>
                    <td style={styles.td}>
                      <div style={styles.proizvodCell}>
                        {slikaUrl ? (
                          <img
                            src={formatirajSlikaUrl(slikaUrl)}
                            alt={getStavkaNaziv(stavka)}
                            style={styles.slika}
                          />
                        ) : (
                          <div style={styles.slikaPlaceholder}>Bez slike</div>
                        )}

                        <div>
                          <strong>{getStavkaNaziv(stavka)}</strong>
                          <p style={styles.proizvodOpis}>
                            {getVrednost(proizvod, ['tipProizvoda', 'TipProizvoda'], '')}
                          </p>
                        </div>
                      </div>
                    </td>

                    <td style={styles.td}>{proizvodId ? `#${proizvodId}` : '-'}</td>
                    <td style={styles.td}>{kolicina} kom</td>
                    <td style={styles.td}>
                      {cena !== '' && cena !== null && cena !== undefined
                        ? `${Number(cena).toLocaleString()} RSD`
                        : '-'}
                    </td>
                    <td style={styles.td}>
                      {iznos !== '' && iznos !== null && iznos !== undefined
                        ? `${Number(iznos).toLocaleString()} RSD`
                        : '-'}
                    </td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        ) : (
          <p style={styles.prazno}>Nema učitanih stavki za ovu porudžbinu.</p>
        )}
      </section>
    </main>
  );
}

function Info({ label, value }) {
  return (
    <div style={styles.infoItem}>
      <span style={styles.infoLabel}>{label}</span>
      <strong style={styles.infoValue}>{value}</strong>
    </div>
  );
}

const styles = {
  page: {
    minHeight: '100vh',
    backgroundColor: '#f4f8fb',
    padding: '70px 6%',
    color: '#002b55'
  },
  backBtn: {
    border: 'none',
    backgroundColor: 'transparent',
    color: '#315f8f',
    fontSize: '15px',
    fontWeight: 700,
    cursor: 'pointer',
    padding: 0,
    marginBottom: '18px'
  },
  headerCard: {
    backgroundColor: 'white',
    borderRadius: '16px',
    padding: '28px',
    marginBottom: '22px',
    boxShadow: '0 10px 30px rgba(0,0,0,0.07)',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'space-between',
    gap: '20px'
  },
  title: {
    margin: 0,
    fontSize: '40px',
    color: '#002b55'
  },
  subtitle: {
    margin: '8px 0 0',
    color: '#5b7896',
    fontSize: '16px'
  },
  poruka: {
    marginBottom: '20px',
    padding: '14px 18px',
    borderRadius: '8px',
    backgroundColor: '#e0f2fe',
    color: '#075985',
    fontWeight: 700
  },
  porukaGreska: {
    padding: '16px 20px',
    borderRadius: '8px',
    backgroundColor: '#fee2e2',
    color: '#991b1b',
    fontWeight: 700
  },
  loading: {
    fontSize: '20px',
    fontWeight: 700,
    textAlign: 'center'
  },
  grid: {
    display: 'grid',
    gridTemplateColumns: '1fr 1fr',
    gap: '22px',
    marginBottom: '22px'
  },
  card: {
    backgroundColor: 'white',
    borderRadius: '16px',
    padding: '26px',
    boxShadow: '0 10px 30px rgba(0,0,0,0.07)'
  },
  cardTitle: {
    margin: '0 0 18px',
    fontSize: '24px',
    color: '#002b55'
  },
  infoItem: {
    display: 'flex',
    justifyContent: 'space-between',
    gap: '18px',
    padding: '12px 0',
    borderBottom: '1px solid #edf2f7'
  },
  infoLabel: {
    color: '#6384a3',
    fontWeight: 700
  },
  infoValue: {
    color: '#002b55',
    textAlign: 'right'
  },
  label: {
    display: 'block',
    marginTop: '18px',
    marginBottom: '8px',
    color: '#6384a3',
    fontWeight: 700
  },
  input: {
    width: '100%',
    padding: '12px',
    border: '1px solid #d9e6f2',
    borderRadius: '8px',
    fontSize: '15px',
    outline: 'none'
  },
  statusBadgeVeliki: {
    display: 'inline-block',
    padding: '12px 18px',
    borderRadius: '999px',
    fontSize: '15px',
    fontWeight: 900,
    whiteSpace: 'nowrap'
  },
  stavkeCard: {
    backgroundColor: 'white',
    borderRadius: '16px',
    padding: '26px',
    boxShadow: '0 10px 30px rgba(0,0,0,0.07)',
    overflowX: 'auto'
  },
  stavkeHeader: {
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    gap: '16px',
    marginBottom: '16px'
  },
  stavkeCount: {
    backgroundColor: '#edf6ff',
    color: '#0f4c81',
    padding: '8px 12px',
    borderRadius: '999px',
    fontWeight: 800
  },
  table: {
    width: '100%',
    borderCollapse: 'collapse',
    minWidth: '850px'
  },
  thead: {
    backgroundColor: '#f3f7fb'
  },
  th: {
    padding: '16px',
    textAlign: 'left',
    fontSize: '14px',
    color: '#527393',
    letterSpacing: '1px'
  },
  tr: {
    borderBottom: '1px solid #edf2f7'
  },
  td: {
    padding: '16px',
    fontSize: '15px',
    verticalAlign: 'middle'
  },
  proizvodCell: {
    display: 'flex',
    alignItems: 'center',
    gap: '14px'
  },
  slika: {
    width: '64px',
    height: '64px',
    borderRadius: '10px',
    objectFit: 'cover',
    backgroundColor: '#eef4fa'
  },
  slikaPlaceholder: {
    width: '64px',
    height: '64px',
    borderRadius: '10px',
    backgroundColor: '#eef4fa',
    color: '#6b7280',
    fontSize: '12px',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    textAlign: 'center'
  },
  proizvodOpis: {
    margin: '4px 0 0',
    color: '#6b7280',
    fontSize: '13px'
  },
  prazno: {
    textAlign: 'center',
    padding: '28px',
    color: '#6b7280'
  }
};

export default DetaljiPorudzbine;
