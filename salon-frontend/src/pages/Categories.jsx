const kategorije = [
  {
    id: 1,
    naziv: "Dnevna soba",
    opis: "Garniture, trosedi, fotelje, klub stolovi i TV komode.",
    ikonica: "🛋️",
  },
  {
    id: 2,
    naziv: "Spavaća soba",
    opis: "Kreveti, ormari, komode i noćni ormarići.",
    ikonica: "🛏️",
  },
  {
    id: 3,
    naziv: "Trpezarija",
    opis: "Trpezarijski stolovi, stolice i setovi za ručavanje.",
    ikonica: "🍽️",
  },
  {
    id: 4,
    naziv: "Kancelarija",
    opis: "Radni stolovi, kancelarijske stolice i police.",
    ikonica: "💻",
  },
  {
    id: 5,
    naziv: "Dečija soba",
    opis: "Dečiji kreveti, ormari i radni stolovi.",
    ikonica: "🧸",
  },
];

function Categories() {
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
            <div style={styles.icon}>{kategorija.ikonica}</div>
            <h3 style={styles.cardTitle}>{kategorija.naziv}</h3>
            <p style={styles.description}>{kategorija.opis}</p>
            <button style={styles.button}>Pogledaj proizvode</button>
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
    gridTemplateColumns: "repeat(auto-fit, minmax(260px, 1fr))",
    gap: "30px",
  },
  card: {
    backgroundColor: "#ffffff",
    borderRadius: "18px",
    padding: "35px",
    textAlign: "center",
    boxShadow: "0 10px 25px rgba(0,0,0,0.08)",
  },
  icon: {
    fontSize: "46px",
    marginBottom: "15px",
  },
  cardTitle: {
    fontSize: "24px",
    color: "#102a43",
  },
  description: {
    color: "#627d98",
    minHeight: "55px",
  },
  button: {
    marginTop: "15px",
    padding: "12px 18px",
    border: "none",
    borderRadius: "10px",
    backgroundColor: "#0b3d91",
    color: "white",
    cursor: "pointer",
  },
};

export default Categories;