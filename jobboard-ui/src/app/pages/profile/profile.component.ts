import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ProfileService } from '../../core/services/profile.service';
import { FileService } from '../../core/services/file.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit {

  profileId = 0;

  skills = '';
  experience = '';
  education = '';

  selectedFile!: File;

  resumeUrl = '';

  constructor(
    private profileService: ProfileService,
    private fileService: FileService
  ) {}

  ngOnInit(): void {

    const userId =
      Number(localStorage.getItem('userId'));

    this.profileService
      .getProfile(userId)
      .subscribe({
        next: (profile: any) => {

          this.profileId = profile.id;

          this.skills = profile.skills;

          this.experience = profile.experience;

          this.education = profile.education;

          this.resumeUrl = profile.resumeUrl;
        },

        error: () => {

          console.log('No profile found');
        }
      });
  }

  get resumeLink(): string {

    return `https://jobboard-api-praveen-f0csekc9adg5gmbb.centralindia-01.azurewebsites.net${this.resumeUrl}`;
  }

  onFileSelected(event: any) {

    this.selectedFile =
      event.target.files[0];
  }

  uploadAndSaveProfile() {

    if (this.selectedFile) {

      this.fileService
        .uploadResume(this.selectedFile)
        .subscribe({
          next: (response: any) => {

            this.resumeUrl =
              response.fileUrl;

            this.saveProfile();
          },

          error: (err) => {

            console.log(err);

            alert('Resume Upload Failed');
          }
        });

    } else {

      this.saveProfile();
    }
  }

  saveProfile() {

    const profile = {

      userId:
        Number(localStorage.getItem('userId')),

      skills: this.skills,

      experience: this.experience,

      education: this.education,

      resumeUrl: this.resumeUrl
    };

    if (this.profileId > 0) {

      this.profileService
        .updateProfile(
          this.profileId,
          profile
        )
        .subscribe({
          next: () => {

            alert('Profile Updated');
          },

          error: (err) => {

            console.log(err);

            alert('Profile Update Failed');
          }
        });

    } else {

      this.profileService
        .createProfile(profile)
        .subscribe({
          next: (response: any) => {

            this.profileId =
              response.id;

            alert('Profile Created');
          },

          error: (err) => {

            console.log(err);

            alert('Profile Creation Failed');
          }
        });
    }
  }
}