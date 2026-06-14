import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

const kategorijeDefault = [
  {
    naziv: "Dnevna soba",
    opis: "Garniture, trosedi, fotelje, klub stolovi i TV komode.",
    slika: "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?w=600&q=80"
  },
  {
    naziv: "Spavaća soba",
    opis: "Kreveti, ormari, komode i noćni ormarići.",
    slika: "https://images.unsplash.com/photo-1588046130717-0eb0c9a3ba15?w=600&q=80"
  },
  {
    naziv: "Trpezarija",
    opis: "Trpezarijski stolovi, stolice i setovi za ručavanje.",
    slika: "https://images.unsplash.com/photo-1617806118233-18e1de247200?w=600&q=80"
  },
  {
    naziv: "Kancelarija",
    opis: "Radni stolovi, kancelarijske stolice i police.",
    slika: "https://images.unsplash.com/photo-1593642632559-0c6d3fc62b89?w=600&q=80"
  },
  {
    naziv: "Dečija soba",
    opis: "Dečiji kreveti, ormari i radni stolovi.",
    slika: "https://images.unsplash.com/photo-1617325247661-675ab4b64ae2?w=600&q=80"
  },
];

function Categories() {
  const [kategorije, setKategorije] = useState([]);
  const navigate = useNavigate();

  const backendUrl = api.defaults.baseURL
    ? api.defaults.baseURL.replace(/\/api\/?$/, '')
    : 'https://localhost:7267';

  const getId = (kategorija) => {
    return (
      kategorija.kategorijaID ||
      kategorija.kategorijaId ||
      kategorija.KategorijaID ||
      kategorija.KategorijaId ||
      kategorija.id ||
      kategorija.Id
    );
  };

  const getNaziv = (kategorija) => {
    return kategorija.naziv || kategorija.Naziv || '';
  };

  const getSlikaUrl = (kategorija) => {
    const url =
      kategorija.slikaUrl ||
      kategorija.SlikaUrl ||
      kategorija.slikaURL ||
      kategorija.SlikaURL ||
      kategorija.slika ||
      kategorija.Slika;

    if (!url) return '';

    if (url.startsWith('http') || url.startsWith('blob:')) {
      return url;
    }

    if (url.startsWith('/')) {
      return `${backendUrl}${url}`;
    }

    return `${backendUrl}/${url}`;
  };

  useEffect(() => {
    api.get('/Kategorije')
      .then(response => {
        if (response.data && response.data.length > 0) {
          const kategorijeIzBaze = response.data.map((k, i) => {
            const defaultKategorija = kategorijeDefault[i % kategorijeDefault.length];

            return {
              ...k,
              id: getId(k),
              naziv: getNaziv(k),
              opis: k.opis || k.Opis || defaultKategorija.opis,
              slika: getSlikaUrl(k) || defaultKategorija.slika
            };
          });

          setKategorije(kategorijeIzBaze);
        } else {
          setKategorije(kategorijeDefault);
        }
      })
      .catch((err) => {
        console.log('Greška pri učitavanju kategorija:', err.response?.data || err.message);
        setKategorije(kategorijeDefault);
      });
  }, []);

  const handlePogledaj = (kategorijaId, naziv) => {
    navigate(`/products?kategorija=${kategorijaId}&naziv=${encodeURIComponent(naziv)}`);
  };

  return (
    <main style={styles.page}>
      <section style={styles.header}>
        <p style={styles.subtitle}>FurnitureShop</p>
        <h1 style={styles.title}>Kategorije nameštaja</h1>
        <p style={styles.text}>
          Pronađite nameštaj prema prostoriji koju uređujete.
        </p>
      </section>

      <section style={styles.grid}>
        {kategorije.map((kategorija) => (
          <div key={kategorija.id} style={styles.card}>
            <div style={styles.imageBox}>
              {kategorija.slika ? (
                <img
                  src={kategorija.slika}
                  alt={kategorija.naziv}
                  style={styles.image}
                  onError={(e) => {
                    e.currentTarget.style.display = 'none';
                  }}
                />
              ) : (
                <div style={styles.noImage}>
                  Nema slike
                </div>
              )}

              <div style={styles.overlay}>
                <h3 style={styles.overlayTitle}>{kategorija.naziv}</h3>
              </div>
            </div>

            <div style={styles.cardBody}>
              <p style={styles.description}>{kategorija.opis}</p>

              <button
                style={styles.button}
                onClick={() => handlePogledaj(kategorija.id, kategorija.naziv)}
              >
                Pogledaj ponudu →
              </button>
            </div>
          </div>
        ))}
      </section>
    </main>
  );
}

const styles = {
  page: {
    minHeight: "100vh",
    padding: "60px",
    background: "linear-gradient(180deg, #f7fbff 0%, #ffffff 100%)",
  },
  header: {
    textAlign: "center",
    marginBottom: "50px",
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
  grid: {
    display: "grid",
    gridTemplateColumns: "repeat(auto-fit, minmax(300px, 1fr))",
    gap: "30px",
  },
  card: {
    backgroundColor: "#ffffff",
    borderRadius: "18px",
    overflow: "hidden",
    boxShadow: "0 10px 25px rgba(0,0,0,0.08)",
    transition: "transform 0.2s",
  },
  imageBox: {
    position: "relative",
    height: "220px",
    overflow: "hidden",
    backgroundColor: "#e0e8f0",
  },
  image: {
    width: "100%",
    height: "100%",
    objectFit: "cover",
  },
  noImage: {
    width: "100%",
    height: "100%",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    color: "#627d98",
    fontWeight: "600",
  },
  overlay: {
    position: "absolute",
    bottom: 0,
    left: 0,
    right: 0,
    background: "linear-gradient(transparent, rgba(11,61,145,0.85))",
    padding: "20px",
  },
  overlayTitle: {
    color: "white",
    fontSize: "22px",
    fontWeight: "700",
    margin: 0,
  },
  cardBody: {
    padding: "25px",
  },
  description: {
    color: "#627d98",
    fontSize: "15px",
    lineHeight: "1.6",
    minHeight: "50px",
  },
  button: {
    marginTop: "15px",
    padding: "12px 20px",
    border: "none",
    borderRadius: "10px",
    backgroundColor: "#0b3d91",
    color: "white",
    cursor: "pointer",
    fontSize: "15px",
    fontWeight: "600",
    width: "100%",
  },
};

export default Categories;