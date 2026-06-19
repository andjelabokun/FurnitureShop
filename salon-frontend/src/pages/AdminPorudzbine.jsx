import { useEffect, useMemo, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import api from '../services/api';

function AdminPorudzbine() {
  const [porudzbine, setPorudzbine] = useState([]);
  const [poruka, setPoruka] = useState('');
  const [pretraga, setPretraga] = useState('');
  const [statusFilter, setStatusFilter] = useState('');
  const [datumOd, setDatumOd] = useState('');
  const [datumDo, setDatumDo] = useState('');
  const [trenutnaStrana, setTrenutnaStrana] = useState(1);

  const BROJ_PO_STRANI = 5;

  const { isAdmin } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (!isAdmin()) {
      navigate('/');
      return;
    }

    ucitajPorudzbine();
  }, []);

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

  const ucitajPorudzbine = async () => {
    try {
      const res = await api.get('/Porudzbine');
      setPorudzbine(Array.isArray(res.data) ? res.data : []);
    } catch (err) {
      console.log('Greška pri učitavanju porudžbina:', err.response?.data || err.message);
      setPoruka('Greška pri učitavanju porudžbina.');
      setTimeout(() => setPoruka(''), 4000);
    }
  };

  const promeniStatus = async (porudzbina, noviStatus) => {
    const id = getPorudzbinaId(porudzbina);
    const ukupanIznos = getVrednost(porudzbina, ['ukupanIznos', 'UkupanIznos'], 0);

    try {
      await api.put(`/Porudzbine/${id}/status`, {
        status: noviStatus,
        ukupanIznos: Number(ukupanIznos) || 0
      });

      setPoruka('Status uspešno promenjen.');
      ucitajPorudzbine();
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

  const formatirajDatum = (datumVreme) => {
    if (!datumVreme) return '-';
    return new Date(datumVreme).toLocaleDateString('sr-RS');
  };

  const formatirajNovac = (iznos) => {
    return `${Number(iznos || 0).toLocaleString()} RSD`;
  };

  const porudzbineIzPoslednjihMesecDana = useMemo(() => {
    const granica = new Date();
    granica.setMonth(granica.getMonth() - 1);

    return porudzbine.filter(p => {
      const datumVreme = getVrednost(p, [
        'datumVreme',
        'DatumVreme',
        'datum',
        'Datum'
      ]);

      if (!datumVreme) {
        return false;
      }

      const datumPorudzbine = new Date(datumVreme);

      return datumPorudzbine >= granica;
    });
  }, [porudzbine]);

  const filtriranePorudzbine = useMemo(() => {
    const tekst = pretraga.trim().toLowerCase();

    return porudzbineIzPoslednjihMesecDana.filter(p => {
      const id = String(getPorudzbinaId(p));
      const status = getVrednost(p, ['status', 'Status'], 'Kreirana');
      const datumVreme = getVrednost(p, ['datumVreme', 'DatumVreme', 'datum', 'Datum']);
      const kupac = `${getKupacIme(p)} ${getKupacPrezime(p)}`.trim();
      const email = getKupacEmail(p);
      const telefon = getKupacTelefon(p);
      const adresa = getKupacAdresa(p);

      const poklapaSePretraga = !tekst || [
        id,
        kupac,
        email,
        telefon,
        adresa,
        status
      ].some(vrednost => String(vrednost).toLowerCase().includes(tekst));

      const poklapaSeStatus = !statusFilter || status === statusFilter;

      let poklapaSeDatum = true;

      if (datumVreme) {
        const datum = new Date(datumVreme);

        if (datumOd) {
          const od = new Date(`${datumOd}T00:00:00`);
          poklapaSeDatum = poklapaSeDatum && datum >= od;
        }

        if (datumDo) {
          const doDatuma = new Date(`${datumDo}T23:59:59`);
          poklapaSeDatum = poklapaSeDatum && datum <= doDatuma;
        }
      }

      return poklapaSePretraga && poklapaSeStatus && poklapaSeDatum;
    });
  }, [porudzbineIzPoslednjihMesecDana, pretraga, statusFilter, datumOd, datumDo]);

  useEffect(() => {
    setTrenutnaStrana(1);
  }, [pretraga, statusFilter, datumOd, datumDo]);

  const ukupnoStrana = Math.max(1, Math.ceil(filtriranePorudzbine.length / BROJ_PO_STRANI));
  const sigurnaStrana = Math.min(trenutnaStrana, ukupnoStrana);
  const pocetak = (sigurnaStrana - 1) * BROJ_PO_STRANI;
  const prikazanePorudzbine = filtriranePorudzbine.slice(pocetak, pocetak + BROJ_PO_STRANI);

  const resetujFiltere = () => {
    setPretraga('');
    setStatusFilter('');
    setDatumOd('');
    setDatumDo('');
  };

  return (
    <main style={styles.page}>
      <div style={styles.headerRow}>
        <div>
          <button style={styles.backBtn} onClick={() => navigate('/admin')}>
            ← Nazad na admin panel
          </button>

          <h1 style={styles.title}>Porudžbine</h1>
          <p style={styles.subtitle}>
            Pregled i pretraga porudžbina iz poslednjih mesec dana.
          </p>
        </div>

        <button style={styles.refreshBtn} onClick={ucitajPorudzbine}>
          Osveži
        </button>
      </div>

      {poruka && (
        <div style={styles.poruka}>
          {poruka}
        </div>
      )}

      <section style={styles.filterBox}>
        <input
          style={styles.input}
          placeholder="Pretraži po kupcu, email-u, telefonu, adresi ili ID-u"
          value={pretraga}
          onChange={(e) => setPretraga(e.target.value)}
        />

        <select
          style={styles.input}
          value={statusFilter}
          onChange={(e) => setStatusFilter(e.target.value)}
        >
          <option value="">Svi statusi</option>
          <option value="Kreirana">Kreirana</option>
          <option value="U obradi">U obradi</option>
          <option value="Isporucena">Isporucena</option>
          <option value="Otkazana">Otkazana</option>
        </select>

        <input
          style={styles.input}
          type="date"
          value={datumOd}
          onChange={(e) => setDatumOd(e.target.value)}
        />

        <input
          style={styles.input}
          type="date"
          value={datumDo}
          onChange={(e) => setDatumDo(e.target.value)}
        />

        <button style={styles.clearBtn} onClick={resetujFiltere}>
          Resetuj
        </button>
      </section>

      <section style={styles.tabelaBox}>
        <div style={styles.infoRed}>
          <strong>Prikazano:</strong> {filtriranePorudzbine.length} od{' '}
          {porudzbineIzPoslednjihMesecDana.length} porudžbina iz poslednjih mesec dana
        </div>

        <table style={styles.table}>
          <colgroup>
            <col style={{ width: '6%' }} />
            <col style={{ width: '9%' }} />
            <col style={{ width: '14%' }} />
            <col style={{ width: '22%' }} />
            <col style={{ width: '13%' }} />
            <col style={{ width: '12%' }} />
            <col style={{ width: '24%' }} />
          </colgroup>

          <thead>
            <tr style={styles.thead}>
              <th style={styles.th}>ID</th>
              <th style={styles.th}>Datum</th>
              <th style={styles.th}>Kupac</th>
              <th style={styles.th}>Kontakt</th>
              <th style={styles.th}>Adresa</th>
              <th style={styles.th}>Stavke / Iznos</th>
              <th style={styles.th}>Status / Akcija</th>
            </tr>
          </thead>

          <tbody>
            {prikazanePorudzbine.map(p => {
              const id = getPorudzbinaId(p);
              const datumVreme = getVrednost(p, ['datumVreme', 'DatumVreme', 'datum', 'Datum']);
              const ukupanIznos = getVrednost(p, ['ukupanIznos', 'UkupanIznos'], 0);
              const status = getVrednost(p, ['status', 'Status'], 'Kreirana');
              const kupac = `${getKupacIme(p)} ${getKupacPrezime(p)}`.trim();
              const email = getKupacEmail(p);
              const telefon = getKupacTelefon(p);
              const adresa = getKupacAdresa(p);
              const stavke = getStavke(p);

              return (
                <tr key={id} style={styles.tr}>
                  <td style={styles.td}>#{id}</td>

                  <td style={styles.td}>{formatirajDatum(datumVreme)}</td>

                  <td style={{ ...styles.td, ...styles.boldCell }}>
                    {kupac || '-'}
                  </td>

                  <td style={styles.td}>
                    <div style={styles.dveLinije}>{email || '-'}</div>
                    <div style={styles.maliTekst}>{telefon || '-'}</div>
                  </td>

                  <td style={styles.td}>{adresa || '-'}</td>

                  <td style={styles.td}>
                    <div style={styles.dveLinije}>{stavke.length} stavki</div>
                    <div style={styles.maliTekst}>{formatirajNovac(ukupanIznos)}</div>
                  </td>

                  <td style={styles.td}>
                    <div style={styles.statusAkcijeBox}>
                      <span
                        style={{
                          ...styles.statusBadge,
                          backgroundColor: statusBoja(status),
                          color: statusTextBoja(status)
                        }}
                      >
                        {status}
                      </span>

                      <div style={styles.akcije}>
                        <button
                          style={styles.detailsBtn}
                          onClick={() => navigate(`/admin/porudzbine/${id}`)}
                        >
                          Detalji
                        </button>

                        <select
                          style={styles.statusSelect}
                          value={status}
                          onChange={(e) => promeniStatus(p, e.target.value)}
                        >
                          <option value="Kreirana">Kreirana</option>
                          <option value="U obradi">U obradi</option>
                          <option value="Isporucena">Isporucena</option>
                          <option value="Otkazana">Otkazana</option>
                        </select>
                      </div>
                    </div>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </table>

        {filtriranePorudzbine.length === 0 && (
          <p style={styles.prazno}>
            Nema porudžbina iz poslednjih mesec dana koje odgovaraju filterima.
          </p>
        )}

        {ukupnoStrana > 1 && (
          <div style={styles.paginacija}>
            <button
              style={{
                ...styles.pageBtn,
                ...(sigurnaStrana === 1 ? styles.pageBtnDisabled : {})
              }}
              disabled={sigurnaStrana === 1}
              onClick={() => setTrenutnaStrana(sigurnaStrana - 1)}
            >
              Prethodna
            </button>

            {Array.from({ length: ukupnoStrana }, (_, index) => {
              const broj = index + 1;

              return (
                <button
                  key={broj}
                  style={{
                    ...styles.pageBtn,
                    ...(sigurnaStrana === broj ? styles.pageBtnAktivan : {})
                  }}
                  onClick={() => setTrenutnaStrana(broj)}
                >
                  {broj}
                </button>
              );
            })}

            <button
              style={{
                ...styles.pageBtn,
                ...(sigurnaStrana === ukupnoStrana ? styles.pageBtnDisabled : {})
              }}
              disabled={sigurnaStrana === ukupnoStrana}
              onClick={() => setTrenutnaStrana(sigurnaStrana + 1)}
            >
              Sledeća
            </button>
          </div>
        )}
      </section>
    </main>
  );
}

const styles = {
  page: {
    minHeight: '100vh',
    backgroundColor: '#f4f8fb',
    padding: '28px 10px',
    color: '#002b55',
    boxSizing: 'border-box',
    width: '100%',
    maxWidth: '100%'
  },
  headerRow: {
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'flex-start',
    gap: '16px',
    marginBottom: '22px',
    width: '100%'
  },
  title: {
    margin: '10px 0 6px',
    fontSize: '38px',
    color: '#002b55'
  },
  subtitle: {
    margin: 0,
    color: '#5b7896',
    fontSize: '15px'
  },
  backBtn: {
    border: 'none',
    backgroundColor: 'transparent',
    color: '#315f8f',
    fontSize: '15px',
    fontWeight: 700,
    cursor: 'pointer',
    padding: 0
  },
  refreshBtn: {
    padding: '11px 16px',
    border: 'none',
    borderRadius: '8px',
    backgroundColor: '#002b55',
    color: 'white',
    fontWeight: 700,
    cursor: 'pointer'
  },
  poruka: {
    marginBottom: '18px',
    padding: '13px 16px',
    borderRadius: '8px',
    backgroundColor: '#e0f2fe',
    color: '#075985',
    fontWeight: 700
  },
  filterBox: {
    display: 'grid',
    gridTemplateColumns: 'minmax(280px, 2fr) minmax(140px, 1fr) minmax(135px, 1fr) minmax(135px, 1fr) auto',
    gap: '10px',
    marginBottom: '20px',
    backgroundColor: 'white',
    padding: '16px',
    borderRadius: '14px',
    boxShadow: '0 8px 24px rgba(0,0,0,0.06)',
    width: '100%',
    boxSizing: 'border-box'
  },
  input: {
    padding: '11px',
    border: '1px solid #d9e6f2',
    borderRadius: '8px',
    fontSize: '14px',
    outline: 'none',
    minWidth: 0
  },
  clearBtn: {
    padding: '11px 14px',
    border: '1px solid #d9e6f2',
    borderRadius: '8px',
    backgroundColor: 'white',
    color: '#002b55',
    fontWeight: 700,
    cursor: 'pointer'
  },
  tabelaBox: {
    backgroundColor: 'white',
    borderRadius: '16px',
    padding: '14px',
    boxShadow: '0 10px 30px rgba(0,0,0,0.07)',
    width: '100%',
    maxWidth: '100%',
    boxSizing: 'border-box',
    overflowX: 'hidden'
  },
  infoRed: {
    marginBottom: '14px',
    color: '#42698f',
    fontSize: '15px'
  },
  table: {
    width: '100%',
    borderCollapse: 'collapse',
    tableLayout: 'fixed'
  },
  thead: {
    backgroundColor: '#f3f7fb'
  },
  th: {
    padding: '12px 8px',
    textAlign: 'left',
    fontSize: '13px',
    color: '#527393',
    letterSpacing: '0.5px',
    whiteSpace: 'normal'
  },
  tr: {
    borderBottom: '1px solid #edf2f7'
  },
  td: {
    padding: '12px 8px',
    fontSize: '13px',
    verticalAlign: 'middle',
    lineHeight: 1.45,
    wordBreak: 'break-word'
  },
  boldCell: {
    fontWeight: 700
  },
  dveLinije: {
    fontWeight: 600,
    wordBreak: 'break-word'
  },
  maliTekst: {
    marginTop: '5px',
    color: '#4b6b8a',
    fontSize: '12.5px',
    wordBreak: 'break-word'
  },
  statusAkcijeBox: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'flex-start',
    gap: '9px'
  },
  statusBadge: {
    display: 'inline-block',
    padding: '7px 10px',
    borderRadius: '999px',
    fontSize: '12px',
    fontWeight: 800,
    whiteSpace: 'nowrap'
  },
  akcije: {
    display: 'flex',
    gap: '7px',
    alignItems: 'center',
    flexWrap: 'wrap'
  },
  detailsBtn: {
    padding: '8px 10px',
    border: 'none',
    borderRadius: '6px',
    backgroundColor: '#0f4c81',
    color: 'white',
    fontSize: '12px',
    fontWeight: 700,
    cursor: 'pointer'
  },
  statusSelect: {
    padding: '7px',
    border: '1px solid #d9e6f2',
    borderRadius: '6px',
    fontSize: '12px',
    maxWidth: '135px'
  },
  prazno: {
    textAlign: 'center',
    padding: '28px',
    color: '#6b7280'
  },
  paginacija: {
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    gap: '8px',
    marginTop: '22px',
    flexWrap: 'wrap'
  },
  pageBtn: {
    padding: '8px 12px',
    border: '1px solid #cfe8ff',
    borderRadius: '6px',
    backgroundColor: 'white',
    color: '#102a43',
    fontSize: '14px',
    fontWeight: 700,
    cursor: 'pointer'
  },
  pageBtnAktivan: {
    backgroundColor: '#102a43',
    color: 'white',
    border: '1px solid #102a43'
  },
  pageBtnDisabled: {
    opacity: 0.45,
    cursor: 'not-allowed'
  }
};

export default AdminPorudzbine;