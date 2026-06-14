import { useState, useEffect } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import api from '../services/api';
import { korpa } from './Cart';
import { useAuth } from '../context/AuthContext';

function Products() {
  const [proizvodi, setProizvodi] = useState([]);
  const [filtrirani, setFiltrirani] = useState([]);
  const [boje, setBoje] = useState([]);
  const [kategorije, setKategorije] = useState([]);
  const [podkategorije, setPodkategorije] = useState([]);
  const [filterOtvoren, setFilterOtvoren] = useState(false);

  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const { isLoggedIn } = useAuth();

  const [filteri, setFilteri] = useState({
    bojaID: '',
    kategorijaID: searchParams.get('kategorija') || '',
    podkategorijaID: '',
    maxCena: '',
    maxSirina: '',
    maxVisina: '',
    maxDubina: '',
  });

  const backendUrl = api.defaults.baseURL
    ? api.defaults.baseURL.replace(/\/api\/?$/, '')
    : 'https://localhost:7267';

  const getSlikaUrl = (proizvod) => {
    const url =
      proizvod.slikaUrl ||
      proizvod.SlikaUrl ||
      proizvod.slikaURL ||
      proizvod.SlikaURL;

    if (!url) return '';

    if (url.startsWith('http')) {
      return url;
    }

    if (url.startsWith('/')) {
      return `${backendUrl}${url}`;
    }

    return `${backendUrl}/${url}`;
  };

  useEffect(() => {
    api.get('/Proizvodi/sa-dimenzijama')
      .then(r => {
        console.log('Proizvodi:', r.data);
        setProizvodi(r.data);
        setFiltrirani(r.data);
      })
      .catch(err => {
        console.log('Greška pri učitavanju proizvoda:', err.response?.data || err.message);
      });

    api.get('/Boje')
      .then(r => setBoje(r.data))
      .catch(err => console.log('Greška pri učitavanju boja:', err.response?.data || err.message));

    api.get('/Kategorije')
      .then(r => setKategorije(r.data))
      .catch(err => console.log('Greška pri učitavanju kategorija:', err.response?.data || err.message));

    api.get('/PodKategorije')
      .then(r => {
        console.log('Podkategorije:', r.data);
        setPodkategorije(r.data);
      })
      .catch(err => console.log('Greška pri učitavanju podkategorija:', err.response?.data || err.message));
  }, []);

  useEffect(() => {
    let rezultat = [...proizvodi];

    if (filteri.bojaID) {
      rezultat = rezultat.filter(p => p.bojaID === parseInt(filteri.bojaID));
    }

    if (filteri.kategorijaID) {
      rezultat = rezultat.filter(p => {
        const pk = podkategorije.find(pk => pk.podkategorijaID === p.podkategorijaID);
        return pk?.kategorijaID === parseInt(filteri.kategorijaID);
      });
    }

    if (filteri.podkategorijaID) {
      rezultat = rezultat.filter(p => p.podkategorijaID === parseInt(filteri.podkategorijaID));
    }

    if (filteri.maxCena) {
      rezultat = rezultat.filter(p => p.cena <= parseFloat(filteri.maxCena));
    }

    if (filteri.maxSirina) {
      rezultat = rezultat.filter(p => p.sirina <= parseFloat(filteri.maxSirina));
    }

    if (filteri.maxVisina) {
      rezultat = rezultat.filter(p => p.visina <= parseFloat(filteri.maxVisina));
    }

    if (filteri.maxDubina) {
      rezultat = rezultat.filter(p => p.dubina <= parseFloat(filteri.maxDubina));
    }

    setFiltrirani(rezultat);
  }, [filteri, proizvodi, podkategorije]);

  const handleFilter = (e) => {
    setFilteri({ ...filteri, [e.target.name]: e.target.value });
  };

  const resetFilteri = () => {
    setFilteri({
      bojaID: '',
      kategorijaID: '',
      podkategorijaID: '',
      maxCena: '',
      maxSirina: '',
      maxVisina: '',
      maxDubina: ''
    });
  };

  const filtriranePodkategorije = filteri.kategorijaID
    ? podkategorije.filter(pk => pk.kategorijaID === parseInt(filteri.kategorijaID))
    : podkategorije;

  const aktivnihFiltera = Object.values(filteri).filter(v => v !== '').length;

  return (
    <main style={styles.page}>
      <section style={styles.header}>
        <p style={styles.subtitle}>Salon nameštaja</p>
        <h1 style={styles.title}>Naši proizvodi</h1>
        <p style={styles.text}>
          Izaberite moderan, kvalitetan i funkcionalan nameštaj za vaš dom.
        </p>
      </section>

      <div style={styles.toolbar}>
        <span style={styles.brojRezultata}>{filtrirani.length} proizvoda</span>

        <button style={styles.filterBtn} onClick={() => setFilterOtvoren(true)}>
          Filteri {aktivnihFiltera > 0 && <span style={styles.badge}>{aktivnihFiltera}</span>}
        </button>
      </div>

      {filterOtvoren && (
        <>
          <div style={styles.backdrop} onClick={() => setFilterOtvoren(false)} />

          <div style={styles.filterDrawer}>
            <div style={styles.drawerHeader}>
              <h3 style={styles.drawerTitle}>Filteri</h3>
              <button style={styles.closeBtn} onClick={() => setFilterOtvoren(false)}>
                ✕
              </button>
            </div>

            <div style={styles.filterSection}>
              <p style={styles.filterLabel}>KATEGORIJA</p>

              <select
                name="kategorijaID"
                value={filteri.kategorijaID}
                onChange={handleFilter}
                style={styles.select}
              >
                <option value="">Sve kategorije</option>
                {kategorije.map(k => (
                  <option key={k.kategorijaID} value={k.kategorijaID}>
                    {k.naziv}
                  </option>
                ))}
              </select>
            </div>

            <div style={styles.filterSection}>
              <p style={styles.filterLabel}>PODKATEGORIJA</p>

              <select
                name="podkategorijaID"
                value={filteri.podkategorijaID}
                onChange={handleFilter}
                style={styles.select}
              >
                <option value="">Sve podkategorije</option>
                {filtriranePodkategorije.map(pk => (
                  <option key={pk.podkategorijaID} value={pk.podkategorijaID}>
                    {pk.naziv}
                  </option>
                ))}
              </select>
            </div>

            <div style={styles.filterSection}>
              <p style={styles.filterLabel}>BOJA</p>

              <div style={styles.bojeGrid}>
                {boje.map(b => (
                  <button
                    key={b.bojaID}
                    style={{
                      ...styles.bojaBtn,
                      border: filteri.bojaID === String(b.bojaID)
                        ? '2px solid #0b3d91'
                        : '2px solid #e0e0e0',
                      fontWeight: filteri.bojaID === String(b.bojaID) ? '700' : '400',
                    }}
                    onClick={() =>
                      setFilteri({
                        ...filteri,
                        bojaID: filteri.bojaID === String(b.bojaID) ? '' : String(b.bojaID)
                      })
                    }
                  >
                    {b.naziv}
                  </button>
                ))}
              </div>
            </div>

            <div style={styles.filterSection}>
              <p style={styles.filterLabel}>MAX CENA (RSD)</p>

              <input
                name="maxCena"
                type="number"
                value={filteri.maxCena}
                onChange={handleFilter}
                placeholder="npr. 100000"
                style={styles.input}
              />
            </div>

            <div style={styles.filterSection}>
              <p style={styles.filterLabel}>DIMENZIJE (max cm)</p>

              <div style={styles.dimenzijeRow}>
                <div>
                  <p style={styles.dimLabel}>Širina</p>
                  <input
                    name="maxSirina"
                    type="number"
                    value={filteri.maxSirina}
                    onChange={handleFilter}
                    placeholder="200"
                    style={styles.inputSmall}
                  />
                </div>

                <div>
                  <p style={styles.dimLabel}>Visina</p>
                  <input
                    name="maxVisina"
                    type="number"
                    value={filteri.maxVisina}
                    onChange={handleFilter}
                    placeholder="100"
                    style={styles.inputSmall}
                  />
                </div>

                <div>
                  <p style={styles.dimLabel}>Dubina</p>
                  <input
                    name="maxDubina"
                    type="number"
                    value={filteri.maxDubina}
                    onChange={handleFilter}
                    placeholder="80"
                    style={styles.inputSmall}
                  />
                </div>
              </div>
            </div>

            <div style={styles.drawerFooter}>
              <button onClick={resetFilteri} style={styles.resetBtn}>
                Resetuj
              </button>

              <button onClick={() => setFilterOtvoren(false)} style={styles.prikaziBtn}>
                Prikaži {filtrirani.length} rezultata
              </button>
            </div>
          </div>
        </>
      )}

      <section style={styles.grid}>
        {filtrirani.length === 0 ? (
          <p style={styles.nema}>Nema proizvoda koji odgovaraju filterima.</p>
        ) : (
          filtrirani.map((proizvod) => {
            const slika = getSlikaUrl(proizvod);

            return (
              <div key={proizvod.proizvodID} style={styles.card}>
                <div style={styles.imageBox}>
                  {slika ? (
                    <img
                      src={slika}
                      alt={proizvod.naziv}
                      style={styles.image}
                      onError={(e) => {
                        console.log('Slika se ne učitava:', slika);
                        e.currentTarget.style.display = 'none';
                      }}
                    />
                  ) : (
                    <span style={styles.imageText}>{proizvod.naziv}</span>
                  )}
                </div>

                <div style={styles.cardBody}>
                  <h3 style={styles.cardTitle}>{proizvod.naziv}</h3>

                  <p style={styles.description}>{proizvod.opis}</p>

                  <p style={styles.price}>
                    {proizvod.cena?.toLocaleString()} RSD
                  </p>

                  <button
                    style={styles.button}
                    onClick={() => {
                      if (!isLoggedIn()) {
                        navigate('/login');
                        return;
                      }

                      korpa.addItem(proizvod);
                      navigate('/cart');
                    }}
                  >
                    Dodaj u korpu
                  </button>

                  <button
                    style={{
                      ...styles.button,
                      backgroundColor: 'transparent',
                      border: '1px solid #102a43',
                      color: '#102a43',
                      marginTop: '8px'
                    }}
                    onClick={() => navigate(`/products/${proizvod.proizvodID}`)}
                  >
                    Pogledaj detalje
                  </button>
                </div>
              </div>
            );
          })
        )}
      </section>
    </main>
  );
}

const styles = {
  page: {
    minHeight: "100vh",
    padding: "60px 80px",
    background: "linear-gradient(180deg, #f7fbff 0%, #ffffff 100%)",
  },
  header: {
    textAlign: "center",
    marginBottom: "40px",
  },
  subtitle: {
    color: "#0b3d91",
    fontWeight: "600",
    letterSpacing: "2px",
    textTransform: "uppercase",
    margin: 0,
  },
  title: {
    fontSize: "46px",
    color: "#102a43",
    margin: "10px 0",
  },
  text: {
    fontSize: "18px",
    color: "#52616b",
  },
  toolbar: {
    display: "flex",
    justifyContent: "space-between",
    alignItems: "center",
    marginBottom: "30px",
    borderBottom: "1px solid #e0e8f0",
    paddingBottom: "15px",
  },
  brojRezultata: {
    color: "#627d98",
    fontSize: "15px",
  },
  filterBtn: {
    display: "flex",
    alignItems: "center",
    gap: "8px",
    padding: "10px 24px",
    border: "1.5px solid #102a43",
    borderRadius: "4px",
    backgroundColor: "transparent",
    color: "#102a43",
    fontSize: "14px",
    fontWeight: "600",
    letterSpacing: "1px",
    cursor: "pointer",
  },
  badge: {
    backgroundColor: "#0b3d91",
    color: "white",
    borderRadius: "50%",
    width: "20px",
    height: "20px",
    display: "inline-flex",
    alignItems: "center",
    justifyContent: "center",
    fontSize: "12px",
  },
  backdrop: {
    position: "fixed",
    top: 0,
    left: 0,
    right: 0,
    bottom: 0,
    backgroundColor: "rgba(0,0,0,0.4)",
    zIndex: 100,
  },
  filterDrawer: {
    position: "fixed",
    top: 0,
    right: 0,
    width: "400px",
    height: "100vh",
    backgroundColor: "white",
    zIndex: 101,
    padding: "40px",
    overflowY: "auto",
    boxShadow: "-10px 0 40px rgba(0,0,0,0.15)",
    display: "flex",
    flexDirection: "column",
    gap: "24px",
  },
  drawerHeader: {
    display: "flex",
    justifyContent: "space-between",
    alignItems: "center",
  },
  drawerTitle: {
    fontSize: "22px",
    color: "#102a43",
    margin: 0,
    letterSpacing: "1px",
  },
  closeBtn: {
    background: "none",
    border: "none",
    fontSize: "20px",
    cursor: "pointer",
    color: "#627d98",
  },
  filterSection: {
    borderBottom: "1px solid #f0f0f0",
    paddingBottom: "20px",
  },
  filterLabel: {
    fontSize: "12px",
    fontWeight: "700",
    letterSpacing: "2px",
    color: "#102a43",
    margin: "0 0 12px 0",
  },
  select: {
    width: "100%",
    padding: "12px",
    borderRadius: "4px",
    border: "1px solid #e0e0e0",
    fontSize: "14px",
    color: "#102a43",
    outline: "none",
  },
  bojeGrid: {
    display: "flex",
    flexWrap: "wrap",
    gap: "8px",
  },
  bojaBtn: {
    padding: "8px 16px",
    borderRadius: "4px",
    backgroundColor: "white",
    cursor: "pointer",
    fontSize: "13px",
    color: "#102a43",
  },
  input: {
    width: "100%",
    padding: "12px",
    borderRadius: "4px",
    border: "1px solid #e0e0e0",
    fontSize: "14px",
    color: "#102a43",
    outline: "none",
    boxSizing: "border-box",
  },
  dimenzijeRow: {
    display: "flex",
    gap: "12px",
  },
  dimLabel: {
    fontSize: "12px",
    color: "#627d98",
    margin: "0 0 5px 0",
  },
  inputSmall: {
    width: "100%",
    padding: "10px",
    borderRadius: "4px",
    border: "1px solid #e0e0e0",
    fontSize: "14px",
    color: "#102a43",
    outline: "none",
    boxSizing: "border-box",
  },
  drawerFooter: {
    display: "flex",
    gap: "12px",
    marginTop: "auto",
    paddingTop: "20px",
  },
  resetBtn: {
    flex: 1,
    padding: "14px",
    border: "1.5px solid #102a43",
    borderRadius: "4px",
    backgroundColor: "white",
    color: "#102a43",
    fontSize: "14px",
    fontWeight: "600",
    cursor: "pointer",
  },
  prikaziBtn: {
    flex: 2,
    padding: "14px",
    border: "none",
    borderRadius: "4px",
    backgroundColor: "#102a43",
    color: "white",
    fontSize: "14px",
    fontWeight: "600",
    cursor: "pointer",
  },
  grid: {
    display: "grid",
    gridTemplateColumns: "repeat(auto-fill, minmax(280px, 1fr))",
    gap: "30px",
  },
  card: {
    backgroundColor: "#ffffff",
    borderRadius: "12px",
    overflow: "hidden",
    boxShadow: "0 4px 15px rgba(0,0,0,0.06)",
  },
  imageBox: {
    height: "220px",
    background: "linear-gradient(135deg, #cfe8ff, #eaf4ff)",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    overflow: "hidden",
  },
  image: {
    width: "100%",
    height: "100%",
    objectFit: "cover",
    display: "block",
  },
  imageText: {
    color: "#0b3d91",
    fontWeight: "700",
    fontSize: "18px",
    textAlign: "center",
    padding: "10px",
  },
  cardBody: {
    padding: "25px",
  },
  cardTitle: {
    fontSize: "18px",
    color: "#102a43",
    margin: "0 0 8px 0",
    fontWeight: "600",
  },
  description: {
    color: "#627d98",
    fontSize: "14px",
    lineHeight: "1.5",
  },
  price: {
    fontSize: "20px",
    fontWeight: "700",
    color: "#0b3d91",
    margin: "12px 0",
  },
  button: {
    padding: "12px 18px",
    border: "none",
    borderRadius: "6px",
    backgroundColor: "#102a43",
    color: "white",
    cursor: "pointer",
    fontSize: "14px",
    fontWeight: "600",
    width: "100%",
  },
  nema: {
    color: "#627d98",
    fontSize: "18px",
    gridColumn: "1 / -1",
    textAlign: "center",
    marginTop: "50px",
  },
};

export default Products;