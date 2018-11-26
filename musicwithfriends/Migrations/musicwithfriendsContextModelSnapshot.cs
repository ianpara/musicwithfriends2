﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using musicwithfriends.Models;

namespace musicwithfriends.Migrations
{
    [DbContext(typeof(musicwithfriendsContext))]
    partial class musicwithfriendsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("musicwithfriends.Models.Chatroom", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Chatroom");
                });

            modelBuilder.Entity("musicwithfriends.Models.Song", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChatRoomID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("ChatRoomID");

                    b.ToTable("Song");
                });

            modelBuilder.Entity("musicwithfriends.Models.Song", b =>
                {
                    b.HasOne("musicwithfriends.Models.Chatroom")
                        .WithMany("Songs")
                        .HasForeignKey("ChatRoomID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
