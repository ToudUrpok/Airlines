namespace SAMAirline.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Install : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aircraft",
                c => new
                    {
                        AircraftId = c.Int(nullable: false, identity: true),
                        AircraftCodeIATA = c.String(nullable: false, maxLength: 3),
                        AircraftCodeICAO = c.String(nullable: false, maxLength: 4),
                        Model = c.String(nullable: false, maxLength: 100),
                        TotalPlaces = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AircraftId);
            
            CreateTable(
                "dbo.Flight",
                c => new
                    {
                        FlightId = c.Int(nullable: false, identity: true),
                        FlightNumber = c.String(nullable: false, maxLength: 15),
                        AirlineCode = c.String(nullable: false, maxLength: 4),
                        AirlineId = c.Int(nullable: false),
                        DepartureDateTime = c.DateTime(nullable: false),
                        DepartureAirportCode = c.String(nullable: false, maxLength: 3),
                        DepartureAirportId = c.Int(nullable: false),
                        AircraftCode = c.String(nullable: false, maxLength: 3),
                        AircraftId = c.Int(nullable: false),
                        TotalPlaces = c.Int(nullable: false),
                        ArrivalDateTime = c.DateTime(nullable: false),
                        ArrivalAirportCode = c.String(nullable: false, maxLength: 3),
                        ArrivalAirportId = c.Int(nullable: false),
                        Stops = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FlightId)
                .ForeignKey("dbo.Airlines", t => t.AirlineId)
                .ForeignKey("dbo.Airport", t => t.ArrivalAirportId)
                .ForeignKey("dbo.Airport", t => t.DepartureAirportId)
                .ForeignKey("dbo.Aircraft", t => t.AircraftId)
                .Index(t => t.AirlineId)
                .Index(t => t.DepartureAirportId)
                .Index(t => t.AircraftId)
                .Index(t => t.ArrivalAirportId);
            
            CreateTable(
                "dbo.Airlines",
                c => new
                    {
                        AirlineId = c.Int(nullable: false, identity: true),
                        AirlineCodeIATA = c.String(nullable: false, maxLength: 2),
                        AirlineCodeICAO = c.String(nullable: false, maxLength: 3),
                        Name = c.String(nullable: false, maxLength: 50),
                        Country = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.AirlineId);
            
            CreateTable(
                "dbo.Airport",
                c => new
                    {
                        AirportId = c.Int(nullable: false, identity: true),
                        AirportCodeIATA = c.String(nullable: false, maxLength: 3),
                        AirportCodeICAO = c.String(nullable: false, maxLength: 4),
                        Name = c.String(nullable: false, maxLength: 100),
                        Country = c.String(nullable: false, maxLength: 50),
                        City = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.AirportId);
            
            CreateTable(
                "dbo.Booking",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        FlightId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        PassengerName = c.String(nullable: false),
                        PassengerSurname = c.String(nullable: false),
                        PassengerNationality = c.String(nullable: false),
                        PassengerBirthdate = c.String(nullable: false),
                        PassengerPassport = c.String(nullable: false),
                        PassportExpire = c.String(nullable: false),
                        BookingDateTime = c.DateTime(nullable: false),
                        ContactPhoneNumber = c.String(nullable: false),
                        ContactEmail = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Flight", t => t.FlightId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.FlightId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        ProfileImage = c.Binary(),
                        Name = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        Role = c.String(nullable: false),
                        IsConfirmed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        NotificationId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Message = c.String(nullable: false),
                        Flight_FlightId = c.Int(),
                    })
                .PrimaryKey(t => t.NotificationId)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.Flight", t => t.Flight_FlightId)
                .Index(t => t.UserId)
                .Index(t => t.Flight_FlightId);
            
            CreateTable(
                "dbo.LoggingErrors",
                c => new
                    {
                        ErrorId = c.Int(nullable: false, identity: true),
                        ExceptionMessage = c.String(),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                        StackTrace = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ErrorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Flight", "AircraftId", "dbo.Aircraft");
            DropForeignKey("dbo.Notification", "Flight_FlightId", "dbo.Flight");
            DropForeignKey("dbo.Booking", "UserId", "dbo.User");
            DropForeignKey("dbo.Notification", "UserId", "dbo.User");
            DropForeignKey("dbo.Booking", "FlightId", "dbo.Flight");
            DropForeignKey("dbo.Flight", "DepartureAirportId", "dbo.Airport");
            DropForeignKey("dbo.Flight", "ArrivalAirportId", "dbo.Airport");
            DropForeignKey("dbo.Flight", "AirlineId", "dbo.Airlines");
            DropIndex("dbo.Notification", new[] { "Flight_FlightId" });
            DropIndex("dbo.Notification", new[] { "UserId" });
            DropIndex("dbo.Booking", new[] { "UserId" });
            DropIndex("dbo.Booking", new[] { "FlightId" });
            DropIndex("dbo.Flight", new[] { "ArrivalAirportId" });
            DropIndex("dbo.Flight", new[] { "AircraftId" });
            DropIndex("dbo.Flight", new[] { "DepartureAirportId" });
            DropIndex("dbo.Flight", new[] { "AirlineId" });
            DropTable("dbo.LoggingErrors");
            DropTable("dbo.Notification");
            DropTable("dbo.User");
            DropTable("dbo.Booking");
            DropTable("dbo.Airport");
            DropTable("dbo.Airlines");
            DropTable("dbo.Flight");
            DropTable("dbo.Aircraft");
        }
    }
}
