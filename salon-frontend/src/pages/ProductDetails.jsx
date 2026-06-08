import { useParams, Link } from "react-router-dom";

const proizvodi = [
  {
    id: 1,
    naziv: "Ugaona garnitura Roma",
    opis: "Moderna i udobna garnitura za dnevnu sobu.",
    cena: 120000,
    materijal: "Štof",
    boja: "Siva",
    dimenzije: "280 x 180 cm",
    proizvodjac: "FurnitureShop Home",
  },
  {
    id: 2,
    naziv: "Krevet Luna",
    opis: "Elegantni bračni krevet sa kvalitetnom izradom.",
    cena: 85000,
    materijal: "Drvo",
    boja: "Bež",
    dimenzije: "200 x 160 cm",
    proizvodjac: "Dream Line",
  },
  {
    id: 3,
    naziv: "Orman Lux",
    opis: "Prostran orman modernog dizajna.",
    cena: 65000,
    materijal: "Medijapan",
    boja: "Bela",
    dimenzije: "220 x 180 cm",
    proizvodjac: "Lux Design",
  },
];

function ProductDetails() {
  const { id } = useParams();

  const proizvod = proizvodi.find((p) => p.id === Number(id));

  if (!proizvod) {
    return (
      <main style={styles.page}>
        <h1>Proizvod nije pronađen.</h1>
        <Link to="/products">Nazad na proizvode</Link>
      </main>
    );
  }

  return (
    <main style={styles.page}>
      <section style={styles.card}>
        <div style={styles.imageBox}>
          <span style={styles.imageText}>{proizvod.naziv}</span>
        </div>

        <div style={styles.info}>
          <p style={styles.subtitle}>Detalji proizvoda</p>
          <h1 style={styles.title}>{proizvod.naziv}</h1>
          <p style={styles.description}>{proizvod.opis}</p>

          <p style={styles.price}>{proizvod.cena.toLocaleString()} RSD</p>

          <div style={styles.details}>
            <p><strong>Materijal:</strong> {proizvod.materijal}</p>
            <p><strong>Boja:</strong> {proizvod.boja}</p>
            <p><strong>Dimenzije:</strong> {proizvod.dimenzije}</p>
            <p><strong>Proizvođač:</strong> {proizvod.proizvodjac}</p>
          </div>

          <Link to="/products">
            <button style={styles.button}>Nazad na proizvode</button>
          </Link>
        </div>
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
  card: {
    display: "grid",
    gridTemplateColumns: "repeat(auto-fit, minmax(320px, 1fr))",
    gap: "40px",
    backgroundColor: "#ffffff",
    borderRadius: "20px",
    padding: "40px",
    boxShadow: "0 10px 25px rgba(0,0,0,0.08)",
  },
  imageBox: {
    minHeight: "350px",
    borderRadius: "18px",
    background: "linear-gradient(135deg, #cfe8ff, #eaf4ff)",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
  },
  imageText: {
    color: "#0b3d91",
    fontWeight: "700",
    fontSize: "24px",
    textAlign: "center",
  },
  info: {
    display: "flex",
    flexDirection: "column",
    justifyContent: "center",
  },
  subtitle: {
    color: "#0b3d91",
    fontWeight: "600",
    letterSpacing: "2px",
    textTransform: "uppercase",
  },
  title: {
    fontSize: "42px",
    color: "#102a43",
    margin: "10px 0",
  },
  description: {
    color: "#52616b",
    fontSize: "18px",
    lineHeight: "1.7",
  },
  price: {
    fontSize: "26px",
    fontWeight: "700",
    color: "#0b3d91",
  },
  details: {
    color: "#52616b",
    fontSize: "17px",
    marginBottom: "20px",
  },
  button: {
    padding: "14px 20px",
    border: "none",
    borderRadius: "10px",
    backgroundColor: "#0b3d91",
    color: "white",
    fontSize: "16px",
    cursor: "pointer",
  },
};

export default ProductDetails;