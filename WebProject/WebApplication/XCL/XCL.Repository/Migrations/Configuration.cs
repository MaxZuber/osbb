using System.Collections.Generic;
using XCL.Common.Enum;
using XCL.Models.DbModels;

namespace XCL.Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<XCL.Repository.DataContext.OsbbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(XCL.Repository.DataContext.OsbbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            var entrance1 = new Entrance()
            {
                EntranceNumber = 1,
                Flats = new List<Flat>(),
                Sensors = new List<Sensor>()
                {
                    new Sensor()
                    {
                        Dimension = "°C",
                        Description = "Температури води на вході в підїзд",
                        InstallationPosition = SensorInstallationPositionType.Input
                    },
                    new Sensor()
                    {
                        Dimension = "°C",
                        Description = "Температури повітря на вулиці",
                        InstallationPosition = SensorInstallationPositionType.Outer
                    },
                    new Sensor()
                    {
                        Dimension = "°C",
                        Description = "Температури в підїзд",
                        InstallationPosition = SensorInstallationPositionType.Flor
                    }
                }
            };

            var entrance2 = new Entrance()
            {
                EntranceNumber = 2,
                Flats = new List<Flat>(),
                Sensors = new List<Sensor>()
                {
                    new Sensor()
                    {
                        Dimension = "°C",
                        Description = "Температури води на вході в підїзд",
                        InstallationPosition = SensorInstallationPositionType.Input
                    },
                    new Sensor()
                    {
                        Dimension = "°C",
                        Description = "Температури повітря на вулиці",
                        InstallationPosition = SensorInstallationPositionType.Outer
                    },
                    new Sensor()
                    {
                        Dimension = "°C",
                        Description = "Температури в підїзд",
                        InstallationPosition = SensorInstallationPositionType.Flor
                    }
                }
            };

            for (var i = 1; i <= 36; ++i)
            {
                entrance1.Flats.Add(new Flat()
                {
                    FlatNumber = i
                });

                entrance2.Flats.Add(new Flat()
                {
                    FlatNumber = i + 36
                });
            }

            var building = new BuildingInfo
            {
                StreetName = "Вербова",
                StreetNumber = 38,
                Entrances = new List<Entrance>()
                {
                    entrance1,
                    entrance2
                }
            };


            context.BuildingInfos.AddOrUpdate(x => new {x.StreetName, x.StreetNumber},
                building);
        }
    }
}
