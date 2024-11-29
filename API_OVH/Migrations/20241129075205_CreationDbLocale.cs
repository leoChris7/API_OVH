using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API_OVH.Migrations
{
    /// <inheritdoc />
    public partial class CreationDbLocale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BATIMENT",
                columns: table => new
                {
                    BATIMENTID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NOMBATIMENT = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BATIMENT", x => x.BATIMENTID);
                });

            migrationBuilder.CreateTable(
                name: "DIRECTION",
                columns: table => new
                {
                    IDDIRECTION = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LETTRES_DIRECTION = table.Column<string>(type: "varchar(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DIRECTION", x => x.IDDIRECTION);
                    table.CheckConstraint("chk_direction", "\"LETTRES_DIRECTION\" IN ('N', 'S', 'E', 'O', 'NE', 'NO', 'SE', 'SO')");
                });

            migrationBuilder.CreateTable(
                name: "TYPEEQUIPEMENT",
                columns: table => new
                {
                    IDTYPEEQUIPEMENT = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NOMTYPEEQUIPEMENT = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TYPEEQUIPEMENT", x => x.IDTYPEEQUIPEMENT);
                });

            migrationBuilder.CreateTable(
                name: "TYPESALLE",
                columns: table => new
                {
                    IDTYPESALLE = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NOMTYPESALLE = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TYPESALLE", x => x.IDTYPESALLE);
                });

            migrationBuilder.CreateTable(
                name: "UNITE",
                columns: table => new
                {
                    IDUNITE = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NOMUNITE = table.Column<string>(type: "varchar(50)", nullable: false),
                    SIGLEUNITE = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UNITE", x => x.IDUNITE);
                });

            migrationBuilder.CreateTable(
                name: "SALLE",
                columns: table => new
                {
                    IDSALLE = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDBATIMENT = table.Column<int>(type: "integer", nullable: false),
                    IDTYPESALLE = table.Column<int>(type: "integer", nullable: false),
                    NOMSALLE = table.Column<string>(type: "varchar(20)", nullable: false),
                    SUPERFICIESALLE = table.Column<decimal>(type: "numeric(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALLE", x => x.IDSALLE);
                    table.CheckConstraint("chk_salle_superficie", "\"SUPERFICIESALLE\" >= 0");
                    table.ForeignKey(
                        name: "FK_SALLE_EST_QUALI_TYPESALL",
                        column: x => x.IDTYPESALLE,
                        principalTable: "TYPESALLE",
                        principalColumn: "IDTYPESALLE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SALLE_SE_TROUVE_BATIMENT",
                        column: x => x.IDBATIMENT,
                        principalTable: "BATIMENT",
                        principalColumn: "BATIMENTID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CAPTEUR",
                columns: table => new
                {
                    idcapteur = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDSALLE = table.Column<int>(type: "integer", nullable: true),
                    NOMTYPECAPTEUR = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    ESTACTIF = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false, defaultValue: "NSP"),
                    XCAPTEUR = table.Column<decimal>(type: "numeric(10,1)", nullable: false, defaultValue: 0m),
                    YCAPTEUR = table.Column<decimal>(type: "numeric(10,1)", nullable: false, defaultValue: 0m),
                    ZCAPTEUR = table.Column<decimal>(type: "numeric(10,1)", nullable: false, defaultValue: 0m),
                    SalleIdSalle = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAPTEUR", x => x.idcapteur);
                    table.CheckConstraint("chk_capteur_actif", "\"ESTACTIF\" IN ('OUI', 'NON', 'NSP')");
                    table.ForeignKey(
                        name: "FK_CAPTEUR_REFERENCE_SALLE",
                        column: x => x.IDSALLE,
                        principalTable: "SALLE",
                        principalColumn: "IDSALLE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAPTEUR_SALLE_SalleIdSalle",
                        column: x => x.SalleIdSalle,
                        principalTable: "SALLE",
                        principalColumn: "IDSALLE");
                });

            migrationBuilder.CreateTable(
                name: "EQUIPEMENT",
                columns: table => new
                {
                    IDEQUIPEMENT = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDSALLE = table.Column<int>(type: "integer", nullable: false),
                    IDTYPEEQUIPEMENT = table.Column<int>(type: "integer", nullable: false),
                    NOMEQUIPEMENT = table.Column<string>(type: "varchar(20)", nullable: false),
                    LONGUEUR = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 0m),
                    LARGEUR = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 0m),
                    HAUTEUR = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 0m),
                    XEQUIPEMENT = table.Column<decimal>(type: "numeric(10,1)", nullable: false, defaultValue: 0m),
                    YEQUIPEMENT = table.Column<decimal>(type: "numeric(10,1)", nullable: false, defaultValue: 0m),
                    ZEQUIPEMENT = table.Column<decimal>(type: "numeric(10,1)", nullable: false, defaultValue: 0m),
                    ESTACTIF = table.Column<string>(type: "char(3)", nullable: false, defaultValue: "NSP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EQUIPEMENT", x => x.IDEQUIPEMENT);
                    table.CheckConstraint("chk_equip_actif", "\"ESTACTIF\" IN ('OUI', 'NON', 'NSP')");
                    table.CheckConstraint("chk_equip_haut", "\"HAUTEUR\" >= 0");
                    table.CheckConstraint("chk_equip_larg", "\"LARGEUR\" >= 0");
                    table.CheckConstraint("chk_equip_long", "\"LONGUEUR\" >= 0");
                    table.ForeignKey(
                        name: "FK_EQUIPEME_EST_DANS_SALLE",
                        column: x => x.IDSALLE,
                        principalTable: "SALLE",
                        principalColumn: "IDSALLE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EQUIPEME_EST_TYPEEQUI",
                        column: x => x.IDTYPEEQUIPEMENT,
                        principalTable: "TYPEEQUIPEMENT",
                        principalColumn: "IDTYPEEQUIPEMENT",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MUR",
                columns: table => new
                {
                    IDMUR = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDDIRECTION = table.Column<short>(type: "smallint", nullable: false),
                    IDSALLE = table.Column<int>(type: "integer", nullable: false),
                    LONGUEUR = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 0m),
                    HAUTEUR = table.Column<decimal>(type: "numeric(4,2)", nullable: false, defaultValue: 0m),
                    ORIENTATION = table.Column<decimal>(type: "numeric(8,6)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MUR", x => x.IDMUR);
                    table.CheckConstraint("chk_hauteur", "\"HAUTEUR\" > 0");
                    table.CheckConstraint("chk_longueur", "\"LONGUEUR\" > 0");
                    table.CheckConstraint("chk_orientation_degrees", "\"ORIENTATION\" >= 0 AND \"ORIENTATION\" <= 360");
                    table.ForeignKey(
                        name: "FK_MUR_CONTIENT_SALLE",
                        column: x => x.IDSALLE,
                        principalTable: "SALLE",
                        principalColumn: "IDSALLE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MUR_EST_ORIEN_DIRECTIO",
                        column: x => x.IDDIRECTION,
                        principalTable: "DIRECTION",
                        principalColumn: "IDDIRECTION",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UNITE_CAPTEUR",
                columns: table => new
                {
                    IDCAPTEUR = table.Column<int>(type: "integer", nullable: false),
                    IDUNITE = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UNITE_CAPTEUR", x => new { x.IDCAPTEUR, x.IDUNITE });
                    table.ForeignKey(
                        name: "FK_UNICAPT_CAPT",
                        column: x => x.IDCAPTEUR,
                        principalTable: "CAPTEUR",
                        principalColumn: "idcapteur",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UNICAPT_UNI",
                        column: x => x.IDUNITE,
                        principalTable: "UNITE",
                        principalColumn: "IDUNITE",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAPTEUR_IDSALLE",
                table: "CAPTEUR",
                column: "IDSALLE");

            migrationBuilder.CreateIndex(
                name: "IX_CAPTEUR_SalleIdSalle",
                table: "CAPTEUR",
                column: "SalleIdSalle");

            migrationBuilder.CreateIndex(
                name: "IX_EQUIPEMENT_IDSALLE",
                table: "EQUIPEMENT",
                column: "IDSALLE");

            migrationBuilder.CreateIndex(
                name: "IX_EQUIPEMENT_IDTYPEEQUIPEMENT",
                table: "EQUIPEMENT",
                column: "IDTYPEEQUIPEMENT");

            migrationBuilder.CreateIndex(
                name: "IX_MUR_IDDIRECTION",
                table: "MUR",
                column: "IDDIRECTION");

            migrationBuilder.CreateIndex(
                name: "IX_MUR_IDSALLE",
                table: "MUR",
                column: "IDSALLE");

            migrationBuilder.CreateIndex(
                name: "IX_SALLE_IDBATIMENT",
                table: "SALLE",
                column: "IDBATIMENT");

            migrationBuilder.CreateIndex(
                name: "IX_SALLE_IDTYPESALLE",
                table: "SALLE",
                column: "IDTYPESALLE");

            migrationBuilder.CreateIndex(
                name: "IX_UNITE_CAPTEUR_IDUNITE",
                table: "UNITE_CAPTEUR",
                column: "IDUNITE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EQUIPEMENT");

            migrationBuilder.DropTable(
                name: "MUR");

            migrationBuilder.DropTable(
                name: "UNITE_CAPTEUR");

            migrationBuilder.DropTable(
                name: "TYPEEQUIPEMENT");

            migrationBuilder.DropTable(
                name: "DIRECTION");

            migrationBuilder.DropTable(
                name: "CAPTEUR");

            migrationBuilder.DropTable(
                name: "UNITE");

            migrationBuilder.DropTable(
                name: "SALLE");

            migrationBuilder.DropTable(
                name: "TYPESALLE");

            migrationBuilder.DropTable(
                name: "BATIMENT");
        }
    }
}
