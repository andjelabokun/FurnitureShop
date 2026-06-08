function About() {
  return (
    <main style={styles.page}>
      <section style={styles.hero}>
        <p style={styles.subtitle}>O nama</p>
        <h1 style={styles.title}>FurnitureShop</h1>
        <p style={styles.text}>
          FurnitureShop je salon nameštaja namenjen svima koji žele moderan,
          kvalitetan i funkcionalan nameštaj za svoj dom.
        </p>
      </section>

      <section style={styles.content}>
        <div style={styles.infoBox}>
          <h2 style={styles.sectionTitle}>Kontakt informacije</h2>
          <p>📍 Bulevar kralja Aleksandra 73, Beograd</p>
          <p>📞 +381 11 123 4567</p>
          <p>✉️ info@furnitureshop.rs</p>
          <p>🕒 Ponedeljak - Subota: 09:00 - 20:00</p>
        </div>

        <form style={styles.form}>
          <h2 style={styles.sectionTitle}>Pošaljite poruku</h2>

          <input style={styles.input} type="text" placeholder="Ime" />
          <input style={styles.input} type="email" placeholder="Email" />
          <textarea style={styles.textarea} placeholder="Poruka" />

          <button style={styles.button} type="button">
            Pošalji
          </button>
        </form>
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
  hero: {
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
    maxWidth: "750px",
    margin: "0 auto",
    lineHeight: "1.7",
  },
  content: {
    display: "grid",
    gridTemplateColumns: "repeat(auto-fit, minmax(320px, 1fr))",
    gap: "30px",
  },
  infoBox: {
    backgroundColor: "#ffffff",
    borderRadius: "18px",
    padding: "35px",
    boxShadow: "0 10px 25px rgba(0,0,0,0.08)",
    color: "#52616b",
    fontSize: "18px",
  },
  form: {
    backgroundColor: "#ffffff",
    borderRadius: "18px",
    padding: "35px",
    boxShadow: "0 10px 25px rgba(0,0,0,0.08)",
    display: "flex",
    flexDirection: "column",
    gap: "15px",
  },
  sectionTitle: {
    color: "#102a43",
    marginBottom: "15px",
  },
  input: {
    padding: "14px",
    borderRadius: "10px",
    border: "1px solid #d9e2ec",
    fontSize: "16px",
  },
  textarea: {
    padding: "14px",
    borderRadius: "10px",
    border: "1px solid #d9e2ec",
    fontSize: "16px",
    minHeight: "120px",
    resize: "vertical",
  },
  button: {
    padding: "14px",
    border: "none",
    borderRadius: "10px",
    backgroundColor: "#0b3d91",
    color: "white",
    fontSize: "16px",
    cursor: "pointer",
  },
};

export default About;