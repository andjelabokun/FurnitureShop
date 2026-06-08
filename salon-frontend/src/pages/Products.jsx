const proizvodi = [
  { id: 1, naziv: "Ugaona garnitura Roma", cena: 120000, opis: "Moderna i udobna garnitura za dnevnu sobu." },
  { id: 2, naziv: "Krevet Luna", cena: 85000, opis: "Elegantni bračni krevet sa kvalitetnom izradom." },
  { id: 3, naziv: "Orman Lux", cena: 65000, opis: "Prostran orman modernog dizajna." },
];

function Products() {
  return (
    <main style={styles.page}>
      <section style={styles.header}>
        <p style={styles.subtitle}>Salon nameštaja</p>
        <h1 style={styles.title}>Naši proizvodi</h1>
        <p style={styles.text}>
          Izaberite moderan, kvalitetan i funkcionalan nameštaj za vaš dom.
        </p>
      </section>

      <section style={styles.grid}>
        {proizvodi.map((proizvod) => (
          <div key={proizvod.id} style={styles.card}>
            <div style={styles.imageBox}>
              <span style={styles.imageText}>{proizvod.naziv}</span>
            </div>

            <div style={styles.cardBody}>
              <h3 style={styles.cardTitle}>{proizvod.naziv}</h3>
              <p style={styles.description}>{proizvod.opis}</p>
              <p style={styles.price}>{proizvod.cena.toLocaleString()} RSD</p>
              <button style={styles.button}>Pogledaj detalje</button>
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
    gridTemplateColumns: "repeat(auto-fit, minmax(280px, 1fr))",
    gap: "30px",
  },
  card: {
    backgroundColor: "#ffffff",
    borderRadius: "18px",
    overflow: "hidden",
    boxShadow: "0 10px 25px rgba(0,0,0,0.08)",
  },
  imageBox: {
    height: "190px",
    background: "linear-gradient(135deg, #cfe8ff, #eaf4ff)",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
  },
  imageText: {
    color: "#0b3d91",
    fontWeight: "700",
    fontSize: "20px",
  },
  cardBody: {
    padding: "25px",
  },
  cardTitle: {
    fontSize: "22px",
    color: "#102a43",
  },
  description: {
    color: "#627d98",
  },
  price: {
    fontSize: "20px",
    fontWeight: "700",
    color: "#0b3d91",
  },
  button: {
    marginTop: "10px",
    padding: "12px 18px",
    border: "none",
    borderRadius: "10px",
    backgroundColor: "#0b3d91",
    color: "white",
    cursor: "pointer",
  },
};

export default Products;